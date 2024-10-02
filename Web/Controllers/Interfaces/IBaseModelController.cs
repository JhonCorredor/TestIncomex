using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interfaces
{
    public interface IBaseModelController<T, D> where T : BaseModel where D : BaseDto
    {

        /// <summary>
        /// Obtiene todas las entidades y las retorna como una colección de DTOs.
        /// </summary>
        /// <returns>Una colección de todos los DTOs correspondientes a las entidades almacenadas.</returns>
        Task<ActionResult<IEnumerable<D>>> GetAllSelect();

        /// <summary>
        /// Obtiene una entidad específica por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de la entidad.</param>
        /// <returns>El DTO correspondiente a la entidad encontrada, o un error si no se encuentra.</returns>
        Task<ActionResult<D>> GetById(int id);

        /// <summary>
        /// Obtiene una colección de entidades aplicando los filtros especificados.
        /// </summary>
        /// <param name="filters">Filtros a aplicar, como paginación, ordenación, y otros criterios específicos.</param>
        /// <returns>Una colección de DTOs que cumplen con los filtros proporcionados.</returns>
        Task<ActionResult<IEnumerable<D>>> GetDataTable([FromQuery] QueryFilterDto filters);

        /// <summary>
        /// Guarda una nueva entidad en la base de datos basada en el DTO proporcionado.
        /// </summary>
        /// <param name="dto">DTO que contiene la información de la entidad a guardar.</param>
        /// <returns>El DTO correspondiente a la entidad guardada, o un error si la operación falla.</returns>
        Task<ActionResult<D>> Save(D dto);

        /// <summary>
        /// Actualiza una entidad existente en la base de datos basada en el DTO proporcionado.
        /// </summary>
        /// <param name="dto">DTO que contiene la información actualizada de la entidad.</param>
        /// <returns>Un resultado indicando el éxito o el error de la operación de actualización.</returns>
        Task<ActionResult> Update(D dto);

        /// <summary>
        /// Realiza un eliminado lógico de una entidad por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar.</param>
        /// <returns>Un resultado indicando el éxito o el error de la operación de eliminación.</returns>
        Task<ActionResult> Delete(int id);
    }
}
