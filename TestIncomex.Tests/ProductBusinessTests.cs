using AutoMapper; // Importando la biblioteca AutoMapper para la conversión de objetos.
using Business.Implementations; // Importando las implementaciones de la lógica de negocio.
using Business.Interfaces; // Importando interfaces para la lógica de negocio.
using Data.Interfaces; // Importando interfaces para el acceso a datos.
using Entity.Dtos; // Importando Objetos de Transferencia de Datos (DTOs).
using Entity.Dtos.General;
using Entity.Models; // Importando modelos de entidad.
using Moq; // Importando la biblioteca Moq para simular dependencias.

public class ProductBusinessTests
{
    // Instancias simuladas para las dependencias de ProductBusiness
    private readonly Mock<IBaseModelBusiness<Category, CategoryDto>> _mockBusinessCategory; // Simulación de la lógica de negocio de categorías.
    private readonly Mock<IBaseModelBusiness<Suppliers, SuppliersDto>> _mockBusinessSuppliers; // Simulación de la lógica de negocio de proveedores.
    private readonly Mock<IProductData> _mockData; // Simulación del acceso a datos de productos.
    private readonly IMapper _mapper; // Instancia de AutoMapper.

    // Instancia de la clase que se va a probar
    private readonly ProductBusiness _productBusiness; // Instancia de ProductBusiness que se probará.

    // Constructor para configurar los mocks y la instancia de ProductBusiness
    public ProductBusinessTests()
    {
        // Creando mocks para las dependencias
        _mockBusinessCategory = new Mock<IBaseModelBusiness<Category, CategoryDto>>(); // Creando un mock para la lógica de negocio de categorías.
        _mockBusinessSuppliers = new Mock<IBaseModelBusiness<Suppliers, SuppliersDto>>(); // Creando un mock para la lógica de negocio de proveedores.
        _mockData = new Mock<IProductData>(); // Creando un mock para el acceso a datos de productos.

        // Configurando AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            // Añadir tus mapeos de AutoMapper aquí
            cfg.CreateMap<Category, CategoryDto>(); // Mapeo de Category a CategoryDto.
            cfg.CreateMap<Suppliers, SuppliersDto>(); // Mapeo de Suppliers a SuppliersDto.
            // Añadir más mapeos según sea necesario
        });
        _mapper = config.CreateMapper(); // Crear la instancia del mapper

        // Inicializando la instancia de ProductBusiness con las dependencias simuladas
        _productBusiness = new ProductBusiness(
            _mockData.Object,
            _mapper,
            _mockBusinessCategory.Object,
            _mockBusinessSuppliers.Object
        );
    }

    // Caso de prueba para la generación exitosa de productos
    [Fact]
    public async Task GenerateProductsAsync_ValidInput_GeneratesProducts()
    {
        // Arrange: configurando los parámetros de entrada y el comportamiento esperado
        var quantity = 5; // Número de productos a generar
        var categoryId1 = 1; // ID de la primera categoría
        var categoryId2 = 2; // ID de la segunda categoría

        // Configurando las respuestas simuladas de categorías
        var category1 = new CategoryDto { Id = categoryId1, CategoryName = "Category 1" }; // Primer DTO de categoría.
        var category2 = new CategoryDto { Id = categoryId2, CategoryName = "Category 2" }; // Segundo DTO de categoría.

        // Configurando una lista de proveedores
        var suppliers = new List<SuppliersDto>
        {
            new SuppliersDto { Id = 1, HomePage = "Supplier 1" }, // Primer DTO de proveedor.
            new SuppliersDto { Id = 2, HomePage = "Supplier 2" } // Segundo DTO de proveedor.
        };

        // Simulando el comportamiento de los métodos de negocio de categorías
        _mockBusinessCategory.Setup(x => x.GetById(categoryId1)).ReturnsAsync(category1); // Simulación de recuperación de categoría por ID 1.
        _mockBusinessCategory.Setup(x => x.GetById(categoryId2)).ReturnsAsync(category2); // Simulación de recuperación de categoría por ID 2.

        // Simulando la recuperación de la lista de proveedores
        _mockBusinessSuppliers.Setup(x => x.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 })).ReturnsAsync(suppliers); // Simulación de recuperación de todos los proveedores.

        // Simulando el método de guardado de datos
        _mockData.Setup(x => x.Save(It.IsAny<Product>())).ReturnsAsync((Product)null); // Simulación del guardado de cualquier producto.

        // Act: llamando al método bajo prueba
        await _productBusiness.GenerateProductsAsync(quantity, categoryId1, categoryId2); // Ejecutando el método.

        // Assert: verificando que el método de guardado fue llamado el número esperado de veces
        _mockData.Verify(x => x.Save(It.IsAny<Product>()), Times.Exactly(quantity)); // Comprobando si Save fue llamado 'quantity' veces.
    }

    // Caso de prueba para manejar un ID de categoría no válido
    [Fact]
    public async Task GenerateProductsAsync_InvalidCategoryId_ThrowsException()
    {
        // Arrange: configurando los parámetros de entrada
        var quantity = 5; // Número de productos a generar
        var invalidCategoryId = 999; // Un ID de categoría no válido

        // Simulando la recuperación de la categoría para que devuelva null para el ID no válido
        _mockBusinessCategory.Setup(x => x.GetById(invalidCategoryId)).ReturnsAsync((CategoryDto)null); // Configuración para devolver null para ID no válido.

        // Act & Assert: afirmando que se lanza una excepción cuando se llama al método
        await Assert.ThrowsAsync<Exception>(() =>
            _productBusiness.GenerateProductsAsync(quantity, invalidCategoryId, 2)); // Verificando que se lanza una excepción.
    }

    // Caso de prueba para manejar un escenario sin proveedores
    [Fact]
    public async Task GenerateProductsAsync_NoSuppliers_ThrowsException()
    {
        // Arrange: configurando los parámetros de entrada
        var quantity = 5; // Número de productos a generar
        var categoryId1 = 1; // ID de la primera categoría
        var categoryId2 = 2; // ID de la segunda categoría

        // Configurando las categorías simuladas
        var category1 = new CategoryDto { Id = categoryId1, CategoryName = "Category 1" }; // Primer DTO de categoría.
        var category2 = new CategoryDto { Id = categoryId2, CategoryName = "Category 2" }; // Segundo DTO de categoría.

        // Simulando el comportamiento de los métodos de negocio de categorías
        _mockBusinessCategory.Setup(x => x.GetById(categoryId1)).ReturnsAsync(category1); // Simulación de recuperación para la primera categoría.
        _mockBusinessCategory.Setup(x => x.GetById(categoryId2)).ReturnsAsync(category2); // Simulación de recuperación para la segunda categoría.

        // Simulando la recuperación de la lista de proveedores para devolver una lista vacía
        _mockBusinessSuppliers.Setup(x => x.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 })).ReturnsAsync(new List<SuppliersDto>()); // No hay proveedores disponibles.

        // Act & Assert: afirmando que se lanza una excepción cuando se llama al método
        await Assert.ThrowsAsync<Exception>(() =>
            _productBusiness.GenerateProductsAsync(quantity, categoryId1, categoryId2)); // Verificando que se lanza una excepción.
    }
}
