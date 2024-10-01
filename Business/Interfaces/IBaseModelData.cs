using Entity.Dtos;
using Entity.Models;

namespace Data.Interfaces
{
    public interface IBaseModelData <T, D> where T : BaseModel where D : BaseDto
    {
        /// <summary>
        /// Datatable
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<IEnumerable<D>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Obtener
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<D>> GetAllSelect();

        /// <summary>
        /// Obtener por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Save(T entity);

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(T entity);

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task <int> Delete(int id);
    }
}
