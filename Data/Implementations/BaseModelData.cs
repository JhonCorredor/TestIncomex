using AutoMapper;
using Entity.Contexts;
using Entity.Dtos;
using Entity.Dtos.General;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Implementations
{
    public class BaseModelData<T, D> : ABaseModelData<T, D> where T : BaseModel where D : BaseDto
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public BaseModelData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene una colección de todos los objetos en la base de datos en forma de DTOs.
        /// </summary>
        public override async Task<IEnumerable<D>> GetAllSelect()
        {
            try
            {
                var lstModel = await _context.Set<T>().Where(e => e.DeletedAt == null).ToListAsync();

                List<D> lstDto = new List<D>();
                foreach (var item in lstModel)
                {
                    D dto = _mapper.Map<D>(item);
                    lstDto.Add(dto);
                }
                return lstDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recuperar datos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene una colección de objetos en la base de datos aplicando filtros específicos.
        /// </summary>
        public override async Task<IEnumerable<D>> GetDataTable(QueryFilterDto filters)
        {
            IQueryable<T> query = _context.Set<T>().Where(e => e.DeletedAt == null);

            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                query = query.Where(i => EF.Property<int>(i, filters.NameForeignKey) == filters.ForeignKey);
            }

            if (!string.IsNullOrEmpty(filters.Filter))
            {
                query = (IQueryable<T>)PagedListDto<T>.ApplyDynamicFilters(query, filters);
            }

            if (!string.IsNullOrEmpty(filters.ColumnOrder) && !string.IsNullOrEmpty(filters.DirectionOrder))
            {
                query = PagedListDto<T>.ApplyOrdering(query, filters);
            }

            var lstModel = await query.ToListAsync();

            List<D> lstDto = new List<D>();
            foreach (var item in lstModel)
            {
                D dto = _mapper.Map<D>(item);
                lstDto.Add(dto);
            }
            return lstDto;
        }

        /// <summary>
        /// Obtiene una entidad por su identificador único.
        /// </summary>
        public override async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.DeletedAt == null);
        }

        /// <summary>
        /// Guarda una nueva entidad en la base de datos.
        /// </summary>
        public override async Task<T> Save(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        public override async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Realiza un eliminado lógico de una entidad en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar lógicamente.</param>
        /// <returns>Un entero que indica el resultado del proceso de eliminación lógica.</returns>
        public override async Task<int> Delete(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"No se encontró la entidad con el ID {id}.");
            }

            entity.DeletedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
