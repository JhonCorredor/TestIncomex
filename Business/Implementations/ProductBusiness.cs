using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos;
using Entity.Models;
using Entity.Dtos.General;

namespace Business.Implementations
{
    public class ProductBusiness : BaseModelBusiness<Product, ProductDto>, IProductBusiness
    {
        private readonly IProductData _data;
        private readonly IBaseModelBusiness<Category, CategoryDto> _businessCategory;
        private readonly IBaseModelBusiness<Suppliers, SuppliersDto> _businessSuplliers;

        public ProductBusiness(IProductData data, IMapper mapper, IBaseModelBusiness<Category, CategoryDto> businessCategory, IBaseModelBusiness<Suppliers, SuppliersDto> businessSuplliers) : base(data, mapper)
        {
            _data = data;
            _businessCategory = businessCategory;
            _businessSuplliers = businessSuplliers;
        }

        public async Task GenerateProductsAsync(int quantity, int categoryId1, int categoryId2)
        {
            // Obtener la categoría correspondiente al primer ID
            CategoryDto category1 = await _businessCategory.GetById(categoryId1);
            if (category1 == null)
            {
                throw new Exception($"No se encuentra esa categoría en nuestra base de datos = {categoryId1}");
            }

            // Obtener la categoría correspondiente al segundo ID
            CategoryDto category2 = await _businessCategory.GetById(categoryId2);
            if (category2 == null)
            {
                throw new Exception($"No se encuentra esa categoría en nuestra base de datos = {categoryId2}");
            }

            // Obtener la lista de proveedores
            List<SuppliersDto> suppliersDto = await _businessSuplliers.GetAll(new PaginationDto { PageNumber = 0, PageSize = 0});
            if (suppliersDto == null || suppliersDto.Count == 0)
            {
                throw new Exception("No se encuentran registros de Suppliers");
            }

            Random random = new Random();

            // Generar la cantidad especificada de productos
            for (int i = 0; i < quantity; i++)
            {
                // Seleccionar un proveedor aleatorio
                var supplier = suppliersDto[random.Next(0, suppliersDto.Count)];

                // Crear un nuevo producto
                var product = new Product
                {
                    ProductName = $"Producto {i + 1}", // Nombre del producto
                    SupplierId = supplier.Id, // Usar el ID del proveedor seleccionado aleatoriamente
                    CreateAt = DateTime.Now,
                    CategoryId = random.Next(0, 2) == 0 ? categoryId1 : categoryId2,
                    QuantityPerUnit = $"{random.Next(1, 11)} unidades", // Cantidad entre 1 y 10
                    UnitPrice = Convert.ToDecimal(Math.Round(random.NextDouble() * (1000 - 1) + 1, 2)), // Precio entre 1 y 1000
                    UnitsInStock = random.Next(0, 101), // Unidades en stock entre 0 y 100
                    UnitsOnOrder = random.Next(0, 51), // Unidades en orden entre 0 y 50
                    ReorderLevel = random.Next(1, 11), // Nivel de reabastecimiento entre 1 y 10
                    Discontinued = random.Next(0, 2) == 0 // Aleatorio verdadero o falso
                };

                // Guardar el producto en la base de datos
                await _data.Save(product); // Asegúrate de que este método guarde correctamente el producto
            }
        }
    }
}
