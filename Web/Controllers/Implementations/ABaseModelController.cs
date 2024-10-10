using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Interfaces;

namespace Web.Controllers.Implementations
{
    public abstract class ABaseModelController<T, D> : ControllerBase, IBaseModelController<T, D> where T : BaseModel where D : BaseDto
    {
  
        /// <summary>
        /// Elimina una entidad específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar.</param>
        /// <returns>Resultado de la acción de eliminación lógica, indicando si fue exitosa o no.</returns>
        public abstract Task<ActionResult> Delete(int id);

        /// <summary>
        /// Obtiene todas las entidades del tipo especificado y las retorna como una lista de DTOs.
        /// </summary>
        /// <returns>Una lista de DTOs correspondientes a todas las entidades almacenadas.</returns>
        public abstract Task<ActionResult<IEnumerable<D>>> GetAll([FromQuery] PaginationDto pagination);

        /// <summary>
        /// Obtiene una entidad específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a obtener.</param>
        /// <returns>El DTO correspondiente a la entidad encontrada, o un error si no se encuentra.</returns>
        public abstract Task<ActionResult<D>> GetById(int id);
        /// <summary>
        /// Guarda una nueva entidad en la base de datos.
        /// </summary>
        /// <param name="dto">DTO que contiene la información de la entidad a guardar.</param>
        /// <returns>El DTO correspondiente a la entidad guardada, o un error si la operación falla.</returns>
        public abstract Task<ActionResult<D>> Save(D dto);

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="dto">DTO que contiene la información actualizada de la entidad.</param>
        /// <returns>Resultado de la acción de actualización, indicando si fue exitosa o no.</returns>
        public abstract Task<ActionResult> Update(D dto);
    }
}
