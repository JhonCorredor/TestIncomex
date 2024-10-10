using Data.Interfaces;
using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;

namespace Data.Implementations
{
    public abstract class ABaseModelData<T, D> : IBaseModelData<T, D> where T : BaseModel where D : BaseDto
    {
  
        /// <summary>
        /// Realiza un eliminado lógico de una entidad en la base de datos.
        /// Este método debe ser implementado por las clases derivadas.
        /// </summary>
        /// <param name="id">Identificador único de la entidad que se desea eliminar lógicamente.</param>
        /// <returns>Número de filas afectadas por la operación de eliminación lógica.</returns>
        public abstract Task<int> Delete(int id);

        /// <summary>
        /// Obtiene una colección de todos los objetos en la base de datos en forma de DTOs.
        /// Este método debe ser implementado por las clases derivadas.
        /// </summary>
        /// <returns>Una colección de DTOs que representan las entidades almacenadas en la base de datos.</returns>
        public abstract Task<IEnumerable<D>> GetAll(PaginationDto pagination);

        /// <summary>
        /// Obtiene una entidad por su identificador único.
        /// Este método debe ser implementado por las clases derivadas.
        /// </summary>
        /// <param name="id">Identificador único de la entidad que se desea obtener.</param>
        /// <returns>La entidad correspondiente al identificador proporcionado.</returns>
        public abstract Task<D> GetById(int id);

        /// <summary>
        /// Guarda una nueva entidad en la base de datos.
        /// Este método debe ser implementado por las clases derivadas.
        /// </summary>
        /// <param name="entity">La entidad que se desea guardar en la base de datos.</param>
        /// <returns>La entidad guardada.</returns>
        public abstract Task<T> Save(T entity);

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// Este método debe ser implementado por las clases derivadas.
        /// </summary>
        /// <param name="entity">La entidad que se desea actualizar.</param>
        /// <returns>Tarea que representa la operación asincrónica de actualización.</returns>
        public abstract Task Update(T entity);
    }
}
