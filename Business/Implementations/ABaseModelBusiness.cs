using Business.Interfaces;
using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;

namespace Business.Implementations
{
    public abstract class ABaseModelBusiness<T, D> : IBaseModelBusiness<T, D> where T : BaseModel where D : BaseDto
    {
  
        /// <summary>
        /// Realiza un eliminado lógico de una entidad especificada por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar.</param>
        /// <returns>Un entero que indica el resultado del proceso de eliminación lógica.</returns>
        public abstract Task<int> Delete(int id);

        /// <summary>
        /// Obtiene una lista de todos los DTOs correspondientes a las entidades almacenadas.
        /// </summary>
        /// <returns>Lista de DTOs correspondientes a todas las entidades.</returns>
        public abstract Task<List<D>> GetAll(PaginationDto pagination);

        /// <summary>
        /// Obtiene un DTO correspondiente a una entidad, identificada por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de la entidad.</param>
        /// <returns>El DTO correspondiente a la entidad.</returns>
        public abstract Task<D> GetById(int id);

        /// <summary>
        /// Guarda una nueva entidad a partir del DTO proporcionado.
        /// </summary>
        /// <param name="entityDto">El DTO que contiene la información de la entidad a guardar.</param>
        /// <returns>El DTO correspondiente a la entidad guardada.</returns>
        public abstract Task<D> Save(D entityDto);

        /// <summary>
        /// Actualiza una entidad existente a partir del DTO proporcionado.
        /// </summary>
        /// <param name="entityDto">El DTO que contiene la información actualizada de la entidad.</param>
        public abstract Task Update(D entityDto);
    }
}
