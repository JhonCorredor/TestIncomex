using Entity.Dtos;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface IBaseModelController<T, D> where T : BaseModel where D : BaseDto
    {
        
        /// <summary>
        /// Obtiene todas las entidades y las retorna como una colección de DTOs.
        /// </summary>
        Task<ActionResult<IEnumerable<D>>> GetAllSelect();

        /// <summary>
        /// Obtiene una entidad específica por su identificador.
        /// </summary>
        Task<ActionResult<D>> GetById(int id);

        /// <summary>
        /// Obtiene una colección de entidades basadas en los filtros especificados.
        /// </summary>
        Task<ActionResult<IEnumerable<D>>> GetDataTable([FromQuery] QueryFilterDto filters);

        /// <summary>
        /// Guarda una nueva entidad basada en el DTO proporcionado.
        /// </summary>
        Task<ActionResult<D>> Save(D dto);

        /// <summary>
        /// Actualiza una entidad existente basada en el DTO proporcionado.
        /// </summary>
        Task<ActionResult> Update(D dto);

        /// <summary>
        /// Elimina una entidad por su identificador.
        /// </summary>
        Task<ActionResult> Delete(int id);
    }
}