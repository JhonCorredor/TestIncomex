using AutoMapper;
using Business.Implementations;
using Data.Interfaces;
using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;
using Moq;

namespace Data.Implementations
{
    /// <summary>
    /// Modelo de prueba que hereda de BaseModel para usar en las pruebas
    /// </summary>
    public class TestModel : BaseModel {

        public int Id { get; set; }
        public string Nombre { get; set; } 
    }

    /// <summary>
    /// DTO de prueba que hereda de BaseDto para usar en las pruebas
    /// </summary>
    public class TestDto : BaseDto {
        public int Id { get; set; }

        public string Nombre { get; set; }

    }

    /// <summary>
    /// Clase de pruebas unitarias para BaseModelBusiness
    /// </summary>
    public class BaseModelBusinessTests
    {
        private readonly Mock<IBaseModelData<TestModel, TestDto>> _mockData;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BaseModelBusiness<TestModel, TestDto> _business;

        /// <summary>
        /// Constructor que inicializa los mocks y la instancia de negocio para las pruebas
        /// </summary>
        public BaseModelBusinessTests()
        {
            _mockData = new Mock<IBaseModelData<TestModel, TestDto>>();
            _mockMapper = new Mock<IMapper>();
            _business = new BaseModelBusiness<TestModel, TestDto>(_mockData.Object, _mockMapper.Object);
        }

        #region Delete Tests

        /// <summary>
        /// Prueba que verifica que el método Delete retorna el número de filas afectadas
        /// cuando la entidad existe y se elimina correctamente
        /// </summary>
        [Fact]
        public async Task Delete_ReturnsAffectedRows_WhenEntityExists()
        {
            // Arrange: Configura el mock para retornar 1 fila afectada
            var id = 1;
            _mockData.Setup(d => d.Delete(id)).ReturnsAsync(1);

            // Act: Ejecuta el método Delete
            var result = await _business.Delete(id);

            // Assert: Verifica que se retornó 1 y que el método se llamó una vez
            Assert.Equal(1, result);
            _mockData.Verify(d => d.Delete(id), Times.Once);
        }

        /// <summary>
        /// Prueba que verifica que el método Delete lanza una KeyNotFoundException
        /// cuando la entidad a eliminar no existe
        /// </summary>
        [Fact]
        public async Task Delete_ThrowsKeyNotFoundException_WhenEntityDoesNotExist()
        {
            // Arrange: Configura el mock para lanzar KeyNotFoundException
            var id = 1;
            _mockData.Setup(d => d.Delete(id)).ThrowsAsync(new KeyNotFoundException($"No se encontró la entidad con el ID {id}."));

            // Act & Assert: Verifica que se lanza la excepción correcta con el mensaje esperado
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _business.Delete(id));
            Assert.Equal($"No se encontró la entidad con el ID {id}.", exception.Message);
            _mockData.Verify(d => d.Delete(id), Times.Once);
        }

        /// <summary>
        /// Prueba que verifica que el método Delete maneja correctamente los errores
        /// de acceso a datos
        /// </summary>
        [Fact]
        public async Task Delete_ThrowsException_OnDataAccessFailure()
        {
            // Arrange: Configura el mock para lanzar una excepción general
            var id = 1;
            _mockData.Setup(d => d.Delete(id)).ThrowsAsync(new Exception("Error al eliminar la entidad."));

            // Act & Assert: Verifica que se lanza la excepción correcta
            var exception = await Assert.ThrowsAsync<Exception>(() => _business.Delete(id));
            Assert.Equal("Error al eliminar la entidad.", exception.Message);
            _mockData.Verify(d => d.Delete(id), Times.Once);
        }

        #endregion

        #region GetAllSelect Tests

        /// <summary>
        /// Prueba que verifica que GetAllSelect retorna una lista de DTOs
        /// cuando existen datos
        /// </summary>
        [Fact]
        public async Task GetAllSelect_ReturnsListOfDtos_WhenDataExists()
        {
            // Arrange: Prepara una lista de DTOs de prueba
            var testDtos = new List<TestDto>
            {
                new TestDto { Id = 1 },
                new TestDto { Id = 2 }
            };
            _mockData.Setup(d => d.GetAll(new PaginationDto() { PageSize = 0, PageNumber=0})).ReturnsAsync(testDtos);

            // Act: Ejecuta el método GetAllSelect
            var result = await _business.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 });

            // Assert: Verifica que la lista retornada coincide con la esperada
            Assert.Equal(testDtos.Count, result.Count);
            Assert.Equal(testDtos, result);
            _mockData.Verify(d => d.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 }), Times.Once);
        }

        /// <summary>
        /// Prueba que verifica que GetAllSelect retorna una lista vacía
        /// cuando no existen datos
        /// </summary>
        [Fact]
        public async Task GetAllSelect_ReturnsEmptyList_WhenNoDataExists()
        {
            // Arrange: Configura el mock para retornar una lista vacía
            var testDtos = new List<TestDto>();
            _mockData.Setup(d => d.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 })).ReturnsAsync(testDtos);

            // Act: Ejecuta el método GetAllSelect
            var result = await _business.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 });

            // Assert: Verifica que la lista está vacía
            Assert.Empty(result);
            _mockData.Verify(d => d.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 }), Times.Once);
        }

        /// <summary>
        /// Prueba que verifica que GetAllSelect maneja correctamente los errores
        /// de acceso a datos
        /// </summary>
        [Fact]
        public async Task GetAllSelect_ThrowsException_OnDataAccessFailure()
        {
            // Arrange: Configura el mock para lanzar una excepción
            _mockData.Setup(d => d.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 })).ThrowsAsync(new Exception("Error al obtener los datos."));

            // Act & Assert: Verifica que se lanza la excepción correcta
            var exception = await Assert.ThrowsAsync<Exception>(() => _business.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 }));
            Assert.Equal("Error al obtener los datos.", exception.Message);
            _mockData.Verify(d => d.GetAll(new PaginationDto() { PageSize = 0, PageNumber = 0 }), Times.Once);
        }

        #endregion

        #region GetById Tests

        /// <summary>
        /// Prueba que verifica que GetById retorna un DTO cuando la entidad existe
        /// </summary>
        [Fact]
        public async Task GetById_ReturnsDto_WhenEntityExists()
        {
            // Arrange: Prepara un DTO de prueba
            var id = 1;
            var testDto = new TestDto { Id = id };
            _mockData.Setup(d => d.GetById(id)).ReturnsAsync(testDto);

            // Act: Ejecuta el método GetById
            var result = await _business.GetById(id);

            // Assert: Verifica que el DTO retornado coincide con el esperado
            Assert.Equal(testDto, result);
            _mockData.Verify(d => d.GetById(id), Times.Once);
        }

        /// <summary>
        /// Prueba que verifica que GetById retorna null cuando la entidad no existe
        /// </summary>
        [Fact]
        public async Task GetById_ReturnsNull_WhenEntityDoesNotExist()
        {
            // Arrange: Configura el mock para retornar null
            var id = 1;
            _mockData.Setup(d => d.GetById(id)).ReturnsAsync((TestDto)null);

            // Act: Ejecuta el método GetById
            var result = await _business.GetById(id);

            // Assert: Verifica que se retorna null
            Assert.Null(result);
            _mockData.Verify(d => d.GetById(id), Times.Once);
        }

        /// <summary>
        /// Prueba que verifica que GetById maneja correctamente los errores de acceso a datos
        /// </summary>
        [Fact]
        public async Task GetById_ThrowsException_OnDataAccessFailure()
        {
            // Arrange: Configura el mock para lanzar una excepción
            var id = 1;
            _mockData.Setup(d => d.GetById(id)).ThrowsAsync(new Exception("Error al obtener la entidad."));

            // Act & Assert: Verifica que se lanza la excepción correcta
            var exception = await Assert.ThrowsAsync<Exception>(() => _business.GetById(id));
            Assert.Equal("Error al obtener la entidad.", exception.Message);
            _mockData.Verify(d => d.GetById(id), Times.Once);
        }

        #endregion

        #region Save Tests

        /// <summary>
        /// Prueba que verifica que Save establece CreateAt y Activo, y retorna el DTO guardado
        /// </summary>
        [Fact]
        public async Task Save_SetsCreateAtAndActivo_AndReturnsSavedDto()
        {
            // Arrange: Prepara los datos y configura los mocks
            var inputDto = new TestDto();
            var modelToSave = new TestModel();
            var savedModel = new TestModel { Id = 1 };
            var expectedDto = new TestDto { Id = 1 };

            _mockMapper.Setup(m => m.Map<TestModel>(inputDto)).Returns(modelToSave);
            _mockData.Setup(d => d.Save(modelToSave)).ReturnsAsync(savedModel);
            _mockMapper.Setup(m => m.Map<TestDto>(savedModel)).Returns(expectedDto);

            // Act: Ejecuta el método Save
            var result = await _business.Save(inputDto);

            // Assert: Verifica que se establecieron los campos correctamente y se retornó el DTO esperado
            Assert.True(inputDto.Activo);
            Assert.NotNull(inputDto.CreateAt);
            Assert.Equal(expectedDto, result);
            _mockMapper.Verify(m => m.Map<TestModel>(inputDto), Times.Once);
            _mockData.Verify(d => d.Save(modelToSave), Times.Once);
            _mockMapper.Verify(m => m.Map<TestDto>(savedModel), Times.Once);
        }


        /// <summary>
        /// Verifica que el método Save lanza una excepción cuando falla el mapeo de DTO a modelo
        /// </summary>
        [Fact]
        public async Task Save_ThrowsException_WhenMappingFails()
        {
            // Arrange: Configura un DTO de entrada y el mapper para que retorne null
            var inputDto = new TestDto();
            _mockMapper.Setup(m => m.Map<TestModel>(inputDto)).Returns((TestModel)null);

            // Act & Assert: Verifica que se lanza AutoMapperMappingException y que el método Save nunca se llama
            var exception = await Assert.ThrowsAsync<AutoMapperMappingException>(() => _business.Save(inputDto));
            Assert.Equal("El mapeo del DTO a la entidad falló.", exception.Message); // Verifica el mensaje de la excepción
            _mockMapper.Verify(m => m.Map<TestModel>(inputDto), Times.Once);
            _mockData.Verify(d => d.Save(It.IsAny<TestModel>()), Times.Never);
        }


        /// <summary>
        /// Verifica que el método Save maneja correctamente los errores de acceso a datos
        /// </summary>
        [Fact]
        public async Task Save_ThrowsException_OnDataAccessFailure()
        {
            // Arrange: Configura el mapper y el acceso a datos para simular un error al guardar
            var inputDto = new TestDto();
            var modelToSave = new TestModel();
            _mockMapper.Setup(m => m.Map<TestModel>(inputDto)).Returns(modelToSave);
            _mockData.Setup(d => d.Save(modelToSave)).ThrowsAsync(new Exception("Error al guardar la entidad."));

            // Act & Assert: Verifica que se lanza la excepción correcta y que los métodos se llaman según lo esperado
            var exception = await Assert.ThrowsAsync<Exception>(() => _business.Save(inputDto));
            Assert.Equal("Error al guardar la entidad.", exception.Message);
            _mockMapper.Verify(m => m.Map<TestModel>(inputDto), Times.Once);
            _mockData.Verify(d => d.Save(modelToSave), Times.Once);
            _mockMapper.Verify(m => m.Map<TestDto>(It.IsAny<TestModel>()), Times.Never);
        }

        #endregion

        #region Update Tests

        /// <summary>
        /// Verifica que el método Update actualiza correctamente un registro existente
        /// </summary>
        [Fact]
        public async Task Update_UpdatesRecordSuccessfully()
        {
            // Arrange: Prepara el DTO y configura el mapper y el acceso a datos
            var dto = new TestDto
            {
                Id = 1,
                Nombre = "Test",
                Activo = false // Valor inicial
            };

            var entity = new TestModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Activo = true // Valor esperado después de la actualización
            };

            // Configurar el mock para el mapeo
            _mockMapper.Setup(m => m.Map<TestModel>(dto)).Returns(entity);
            _mockData.Setup(d => d.Update(entity)).Returns(Task.CompletedTask);

            // Act: Ejecutar el método Update
            await _business.Update(dto);

            // Assert: Verifica que los métodos se llamaron correctamente
            _mockData.Verify(d => d.Update(entity), Times.Once);
         
        }



        /// <summary>
        /// Verifica que el método Update lanza una excepción cuando falla el mapeo de DTO a modelo
        /// </summary>
        [Fact]
        public async Task Update_ThrowsException_WhenMappingFails()
        {
            // Arrange: Configura el mapper para que retorne null
            var inputDto = new TestDto();
            _mockMapper.Setup(m => m.Map<TestModel>(inputDto)).Returns((TestModel)null);

            // Act & Assert: Verifica que se lanza AutoMapperMappingException y que el método Update nunca se llama
            var exception = await Assert.ThrowsAsync<AutoMapperMappingException>(() => _business.Update(inputDto));

            // Verifica que el mensaje de la excepción es el esperado
            Assert.Equal("El mapeo del DTO a la entidad falló.", exception.Message);

            _mockMapper.Verify(m => m.Map<TestModel>(inputDto), Times.Once);
            _mockData.Verify(d => d.Update(It.IsAny<TestModel>()), Times.Never);
        }


        /// <summary>
        /// Verifica que el método Update maneja correctamente los errores de acceso a datos
        /// </summary>
        [Fact]
        public async Task Update_ThrowsException_OnDataAccessFailure()
        {
            // Arrange: Configura el mapper y el acceso a datos para simular un error al actualizar
            var inputDto = new TestDto();
            var modelToUpdate = new TestModel();
            _mockMapper.Setup(m => m.Map<TestModel>(inputDto)).Returns(modelToUpdate);
            _mockData.Setup(d => d.Update(modelToUpdate)).ThrowsAsync(new Exception("Error al actualizar la entidad."));

            // Act & Assert: Verifica que se lanza la excepción correcta y que los métodos se llaman según lo esperado
            var exception = await Assert.ThrowsAsync<Exception>(() => _business.Update(inputDto));
            Assert.Equal("Error al actualizar la entidad.", exception.Message);
            _mockMapper.Verify(m => m.Map<TestModel>(inputDto), Times.Once);
            _mockData.Verify(d => d.Update(modelToUpdate), Times.Once);

            #endregion


        }
    } }