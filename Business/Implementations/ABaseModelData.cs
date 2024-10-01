using Data.Interfaces;
using Entity.Dtos;
using Entity.Models;

namespace Data.Implementations
{
    public abstract class ABaseModelData<T, D> : IBaseModelData<T, D> where T : BaseModel where D: BaseDto
    {

        /// <summary>
        /// Elimina una entidad del sistema dado su identificador.
        /// </summary>
        public abstract Task<int> Delete(int id);

        /// <summary>
        /// Obtiene todas las entidades como una lista de DTOs.
        /// </summary>
        public abstract Task<IEnumerable<D>> GetAllSelect();

        /// <summary>
        /// Obtiene una entidad específica del sistema dado su identificador.
        /// </summary>
        public abstract Task<T> GetById(int id);

        /// <summary>
        /// Obtiene una colección de entidades basada en los filtros especificados.
        /// </summary>
        public abstract Task<IEnumerable<D>> GetDataTable(QueryFilterDto filters);

        /// <summary>
        /// Guarda una nueva entidad en el sistema.
        /// </summary>
        public abstract Task<T> Save(T entity);

        /// <summary>
        /// Actualiza una entidad existente en el sistema.
        /// </summary>
        public abstract Task Update(T entity);

        
    }
}
