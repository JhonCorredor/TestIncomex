using AutoMapper;
using Data.Interfaces;
using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;

namespace Business.Implementations
{
    public class BaseModelBusiness<T, D> : ABaseModelBusiness<T, D> where T : BaseModel where D : BaseDto
    {
        private readonly IBaseModelData<T, D> _data;
        private readonly IMapper _mapper;

        public BaseModelBusiness(IBaseModelData<T, D> data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        /// <summary>
        /// Realiza un eliminado lógico de una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar.</param>
        /// <returns>El número de registros afectados (1 si el eliminado fue exitoso, 0 si no se encontró).</returns>
        public override async Task<int> Delete(int id)
        {
            return await _data.Delete(id);
        }

        /// <summary>
        /// Obtiene todos los registros seleccionados de la base de datos en formato DTO.
        /// </summary>
        /// <returns>Lista de DTOs que representan las entidades.</returns>
        public override async Task<List<D>> GetAllSelect()
        {
            IEnumerable<D> lstDto = await _data.GetAllSelect();
            return lstDto.ToList();
        }

        /// <summary>
        /// Obtiene un registro por su identificador único y lo convierte a DTO.
        /// </summary>
        /// <param name="id">Identificador único de la entidad.</param>
        /// <returns>DTO que representa la entidad obtenida.</returns>
        public override async Task<D> GetById(int id)
        {
            T entity = await _data.GetById(id);
            D dto = _mapper.Map<D>(entity);
            return dto;
        }

        /// <summary>
        /// Obtiene una lista de registros que cumplen con los filtros proporcionados.
        /// </summary>
        /// <param name="filters">Filtros a aplicar en la consulta de datos.</param>
        /// <returns>Lista de DTOs que cumplen con los filtros.</returns>
        public override async Task<List<D>> GetDataTable(QueryFilterDto filters)
        {
            return (List<D>)await _data.GetDataTable(filters);
        }

        /// <summary>
        /// Guarda un nuevo registro en la base de datos utilizando el DTO proporcionado.
        /// </summary>
        /// <param name="dto">DTO que representa los datos a guardar.</param>
        /// <returns>DTO del registro guardado.</returns>
        public override async Task<D> Save(D dto)
        {
            dto.CreateAt = DateTime.Now; // Establecer la fecha de creación
            dto.Activo = true; // Establecer el campo Activo como verdadero
            BaseModel entity = _mapper.Map<T>(dto); // Mapear DTO a la entidad
            entity = await _data.Save((T)entity); // Guardar la entidad
            return _mapper.Map<D>(entity); // Mapear la entidad guardada de nuevo al DTO
        }

        /// <summary>
        /// Actualiza un registro existente utilizando el DTO proporcionado.
        /// </summary>
        /// <param name="dto">DTO que contiene los datos a actualizar.</param>
        public override async Task Update(D dto)
        {
            dto.CreateAt = DateTime.Now; // Actualizar la fecha de creación
            BaseModel entity = _mapper.Map<T>(dto); // Mapear DTO a la entidad
            await _data.Update((T)entity); // Actualizar la entidad
        }
    }
}
