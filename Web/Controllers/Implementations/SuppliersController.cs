using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Web.Controllers.Implementations
{
    /// <summary>
    ///  Controlador para la entidad <see cref="Suppliers"/>. Hereda las funcionalidades base definidas en <see cref="BaseModelController{Suppliers, SuppliersDto}"/>.
    ///  El cual maneja todas las operaciones relacionadas con las Suppliers.
    /// </summary>
    public class SuppliersController : BaseModelController<Suppliers, SuppliersDto>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SuppliersController"/> con la lógica de negocios específica para proveedores.
        /// </summary>
        /// <param name="business">Interfaz del negocio que proporciona métodos para realizar operaciones CRUD en la entidad <see cref="Suppliers"/>.</param>
        public SuppliersController(IBaseModelBusiness<Suppliers, SuppliersDto> business) : base(business)
        {
        }
    }
}
