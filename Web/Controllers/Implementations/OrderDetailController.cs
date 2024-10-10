using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Web.Controllers.Implementations
{

    /// <summary>
    ///  Controlador para la entidad <see cref="Order"/>. Hereda las funcionalidades base definidas en <see cref="BaseModelController{OrderDetail, OrderDetailDto}"/>.
    ///  El cual maneja todas las operaciones relacionadas con los Detalles del pedido.
    /// </summary>
    public class OrderDetailController : BaseModelController<OrderDetail, OrderDetailDto>
    {

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OrderDetailController"/> con la lógica de negocios específica para Detalle del pedido.
        /// </summary>
        /// <param name="business">Interfaz del negocio que proporciona métodos para realizar operaciones CRUD en la entidad <see cref="OrderDetail"/>.</param>
        public OrderDetailController(IBaseModelBusiness<OrderDetail, OrderDetailDto> business) : base(business)
        {
        }


        
    }
}
