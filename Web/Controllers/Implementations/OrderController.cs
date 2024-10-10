using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Web.Controllers.Implementations
{

    /// <summary>
    ///  Controlador para la entidad <see cref="Order"/>. Hereda las funcionalidades base definidas en <see cref="BaseModelController{Order, OrderDto}"/>.
    ///  El cual maneja todas las operaciones relacionadas con las Ordenes.
    /// </summary>
    public class OrderController : BaseModelController<Order, OrderDto>
    {

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OrderController"/> con la lógica de negocios específica para empleados.
        /// </summary>
        /// <param name="business">Interfaz del negocio que proporciona métodos para realizar operaciones CRUD en la entidad <see cref="Order"/>.</param>
        public OrderController(IBaseModelBusiness<Order, OrderDto> business) : base(business)
        {
        }
    }
}
