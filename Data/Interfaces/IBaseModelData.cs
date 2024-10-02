using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;

namespace Data.Interfaces
{
    public interface IBaseModelData<T, D> where T : BaseModel where D : BaseDto
    {
  
        /// <summary>
        /// Obtiene una lista de DTOs según los filtros especificados.
        /// </summary>
        /// <param name="filters">Filtros aplicables para la consulta, incluidos los parámetros de paginación, ordenación, y otros filtros dinámicos.</param>
        /// <returns>Una lista de DTOs que cumplen con los filtros especificados.</returns>
        Task<IEnumerable<D>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtiene una lista de todos los DTOs correspondientes a las entidades almacenadas.
        /// </summary>
        /// <returns>Una lista de todos los DTOs de las entidades.</returns>
        Task<IEnumerable<D>> GetAllSelect();

        /// <summary>
        /// Obtiene una entidad específica por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a obtener.</param>
        /// <returns>La entidad correspondiente al identificador proporcionado.</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Guarda una nueva entidad en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad que se desea guardar.</param>
        /// <returns>La entidad guardada.</returns>
        Task<T> Save(T entity);

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad que se desea actualizar, con los valores modificados.</param>
        Task Update(T entity);

        /// <summary>
        /// Realiza un eliminado lógico de una entidad especificada por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar lógicamente.</param>
        /// <returns>Un entero que indica el resultado del proceso de eliminación lógica.</returns>
        Task<int> Delete(int id);
    }
}
