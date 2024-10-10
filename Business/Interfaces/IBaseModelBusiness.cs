using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;

namespace Business.Interfaces
{

    public interface IBaseModelBusiness<T, D> where T : BaseModel where D : BaseDto
    {

        /// <summary>
        /// Obtiene todos los registros de la base de datos en formato DTO.
        /// </summary>
        /// <returns>Lista de todos los DTOs correspondientes a la entidad.</returns>
        Task<List<D>> GetAll(PaginationDto pagination);

        /// <summary>
        /// Obtiene un registro específico de la base de datos según su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del registro.</param>
        /// <returns>El DTO que representa la entidad encontrada.</returns>
        Task<D> GetById(int id);

        /// <summary>
        /// Guarda un nuevo registro en la base de datos utilizando el DTO proporcionado.
        /// </summary>
        /// <param name="entityDto">El DTO que contiene los datos del registro a guardar.</param>
        /// <returns>El DTO que representa el registro guardado, incluyendo los datos generados automáticamente (por ejemplo, el ID).</returns>
        Task<D> Save(D entityDto);

        /// <summary>
        /// Actualiza un registro existente en la base de datos utilizando el DTO proporcionado.
        /// </summary>
        /// <param name="entityDto">El DTO que contiene los datos actualizados del registro.</param>
        Task Update(D entityDto);

        /// <summary>
        /// Realiza un eliminado lógico de un registro en la base de datos, marcándolo como eliminado sin borrarlo físicamente.
        /// </summary>
        /// <param name="id">Identificador único del registro a eliminar.</param>
        /// <returns>Un entero que indica si el proceso de eliminación lógica fue exitoso.</returns>
        Task<int> Delete(int id);
    }
}
