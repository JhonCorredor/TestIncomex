using Business.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Web.Controllers.Implementations
{
    /// <summary>
    ///  Controlador para la entidad <see cref="Employee"/>. Hereda las funcionalidades base definidas en <see cref="BaseModelController{Employee, EmployeeDto}"/>.
    ///  El cual maneja todas las operaciones relacionadas con los Employee.
    /// </summary>
    public class EmployeeController : BaseModelController<Employee, EmployeeDto>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmployeeController"/> con la lógica de negocios específica para empleados.
        /// </summary>
        /// <param name="business">Interfaz del negocio que proporciona métodos para realizar operaciones CRUD en la entidad <see cref="Employee"/>.</param>
        public EmployeeController(IBaseModelBusiness<Employee, EmployeeDto> business) : base(business)
        {
        }
    }
}
