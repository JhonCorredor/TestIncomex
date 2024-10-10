using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Web.Controllers.Implementations
{

    /// <summary>
    ///  Controlador para la entidad <see cref="Customer"/>. Hereda las funcionalidades base definidas en <see cref="BaseModelController{Customer, CustomerDto}"/>.
    ///  El cual maneja todas las operaciones relacionadas con las Customer.
    /// </summary>
    public class CustomerController : BaseModelController<Customer, CustomerDto>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CustomerController"/> con la lógica de negocios específica para clientes.
        /// </summary>
        /// <param name="business">Interfaz del negocio que proporciona métodos para realizar operaciones CRUD en la entidad <see cref="Customer"/>.</param>
        public CustomerController(IBaseModelBusiness<Customer, CustomerDto> business) : base(business)
        {
        }
    }
}
