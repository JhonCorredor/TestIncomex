using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Web.Controllers.Implementations

            /// <summary>
            ///  Controlador para la entidad <see cref="Shipper"/>. Hereda las funcionalidades base definidas en <see cref="BaseModelController{Shipper, ShipperDto}"/>.
            ///  El cual maneja todas las operaciones relacionadas con la Expedidora.
            /// </summary>
            /// 
{
    public class ShipperController : BaseModelController<Shipper, ShipperDto>
    {


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OrderDetailController"/> con la lógica de negocios específica para la Expedidora.
        /// </summary>
        /// <param name="business">Interfaz del negocio que proporciona métodos para realizar operaciones CRUD en la entidad <see cref="Shipper"/>.</param>
        public ShipperController(IBaseModelBusiness<Shipper, ShipperDto> business) : base(business)
        {
        }
    }
}
