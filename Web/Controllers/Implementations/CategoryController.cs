using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Web.Controllers.Implementations
{
    /// <summary>
    ///  Controlador para la entidad <see cref="Category"/>. Hereda las funcionalidades base definidas en <see cref="BaseModelController{Category, CategoryDto}"/>.
    ///  El cual maneja todas las operaciones relacionadas con las categorías.
    /// </summary>
    public class CategoryController : BaseModelController<Category, CategoryDto>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CategoryController"/> con la lógica de negocios específica para categorías.
        /// </summary>
        /// <param name="business">Interfaz del negocio que proporciona métodos para realizar operaciones CRUD en la entidad <see cref="Category"/>.</param>
        public CategoryController(IBaseModelBusiness<Category, CategoryDto> business) : base(business)
        {
        }
    }
}
