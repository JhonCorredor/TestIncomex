using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Implementations
{
    /// <summary>
    ///  Controlador para la entidad <see cref="Product"/>. Hereda las funcionalidades base definidas en <see cref="BaseModelController{Product, ProductDto}"/>.
    ///  El cual maneja todas las operaciones relacionadas con las Product.
    /// </summary>
    public class ProductController : BaseModelController<Product, ProductDto>
    {
        /// <summary>
        /// Inyección de dependencia para acceder a los atributos de esa interfaz para proceder a implementarlos en la clase ProductBusiness
        /// </summary>
        private readonly IProductBusiness _business;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ProductController"/> con la lógica de negocios específica para proveedores.
        /// </summary>
        /// <param name="business">Interfaz del negocio que proporciona métodos para realizar operaciones CRUD en la entidad <see cref="Product"/>.</param>
        public ProductController(IBaseModelBusiness<Product, ProductDto> baseBusiness, IProductBusiness business) : base(baseBusiness)
        {
            _business = business;
        }
        /// <summary>
        /// Endpoint encargado de generar productos aleatoriamente, dependiendo del valor que el usuario desee
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="categoryId1"></param>
        /// <param name="categoryId2"></param>
        /// <returns></returns>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateProducts(int quantity, int categoryId1, int categoryId2)
        {
            try
            {
                await _business.GenerateProductsAsync(quantity, categoryId1, categoryId2);
                return Ok(new { Message = $"{quantity} productos generados con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al generar productos.", Error = ex.Message });
            }
        }
        
    }
}
