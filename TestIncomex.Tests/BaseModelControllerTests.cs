using Business.Interfaces;
using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers.Implementations;

namespace TestIncomex.Tests
{
    /// <summary>
    /// Modelo de prueba que hereda de BaseModel para usar en las pruebas
    /// </summary>
    public class TestModel : BaseModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    /// <summary>
    /// DTO de prueba que hereda de BaseDto para usar en las pruebas
    /// </summary>
    public class TestDto : BaseDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
    }

    /// <summary>
    /// Clase de pruebas unitarias para BaseModelController
    /// </summary>
    public class BaseModelControllerTests
    {
        // Mock de la interfaz IBaseModelBusiness
        private readonly Mock<IBaseModelBusiness<TestModel, TestDto>> _mockBusiness;
        // Instancia del controlador a probar
        private readonly BaseModelController<TestModel, TestDto> _controller;

        // Constructor de la clase de pruebas
        public BaseModelControllerTests()
        {
            // Inicialización del mock
            _mockBusiness = new Mock<IBaseModelBusiness<TestModel, TestDto>>();
            // Creación de una instancia del controlador con el mock
            _controller = new BaseModelController<TestModel, TestDto>(_mockBusiness.Object);
        }


       
        #region GetById Tests

        /// <summary>
        /// Prueba para el método GetById
        /// </summary>

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenDataIsNull()
        {
            // Arrange: Configuración del mock para que devuelva null
            _mockBusiness.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync((TestDto)null);

            // Act: Llamada al método que se está probando
            var result = await _controller.GetById(1);

            // Assert: Verificación del resultado
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var response = Assert.IsType<ApiResponse<TestDto>>(notFoundResult.Value);
            Assert.False(response.Status); // Verifica que el estado sea falso
        }

        #endregion

        #region Save Tests

        /// <summary>
        /// Prueba para el método Save
        /// </summary>

        [Fact]
        public async Task Save_ReturnsCreatedAtRoute_WhenSuccessful()
        {
            // Arrange: Preparar el DTO de entrada y configurar el mock
            var inputDto = new TestDto { Id = 1 };
            _mockBusiness.Setup(b => b.Save(It.IsAny<TestDto>())).ReturnsAsync(inputDto);

            // Act: Llamada al método que se está probando
            var result = await _controller.Save(inputDto);

            // Assert: Verificación del resultado
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            var response = Assert.IsType<ApiResponse<TestDto>>(createdAtRouteResult.Value);
            Assert.True(response.Status); // Verifica que el estado sea verdadero
            Assert.Equal(inputDto, response.Data); // Verifica que los datos devueltos sean los esperados
        }

        #endregion

        #region Update Tests

        /// <summary>
        /// Prueba para el método Update
        /// </summary>

        [Fact]
        public async Task Update_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange: Preparar el DTO de entrada y configurar el mock
            var inputDto = new TestDto { Id = 1, Nombre = "Updated Name" };
            _mockBusiness.Setup(b => b.Update(It.IsAny<TestDto>())).Returns(Task.CompletedTask);

            // Act: Llamada al método que se está probando
            var result = await _controller.Update(inputDto);

            // Assert: Verificación del resultado
            var okResult = Assert.IsType<OkObjectResult>(result); // Verifica que el resultado sea OkObjectResult
            var response = Assert.IsType<ApiResponse<TestDto>>(okResult.Value); // Verifica el contenido de la respuesta

            // Verifica que el estado sea verdadero
            Assert.True(response.Status);
            // Verifica el mensaje de éxito
            Assert.Equal("Registro actualizado exitosamente", response.Message);
        }

        #endregion

        #region Delete Tests

        /// <summary>
        /// Prueba para el método Delete
        /// </summary>


        [Fact]
        public async Task Delete_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange: Configuración del mock para que devuelva un registro afectado
            _mockBusiness.Setup(b => b.Delete(It.IsAny<int>())).ReturnsAsync(1);

            // Act: Llamada al método que se está probando
            var result = await _controller.Delete(1);
            var successResponse = new ApiResponse<TestDto>(null, true, "Registro eliminado exitosamente");

            // Assert: Verificación del resultado
            Assert.True(successResponse.Status); // Verifica que el estado sea verdadero
            Assert.Equal("Registro eliminado exitosamente", successResponse.Message); // Verifica el mensaje de éxito
        }
        #endregion


    }
}
