using AutoMapper;
using Entity.Contexts;
using Entity.Dtos;
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

        /// <summary>
        /// Constructor de la clase que inicializa las dependencias del contexto de base de datos, la configuración y el mapeador.
        /// </summary>
        public BaseModelData(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las entidades en forma de lista de DTOs.
        /// </summary>
        public override async Task<IEnumerable<D>> GetAllSelect()
        {
            try
            {
                var lstModel = await _context.Set<T>().ToListAsync();

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
        /// Obtiene una colección de entidades basada en filtros específicos.
        /// </summary>
        public override async Task<IEnumerable<D>> GetDataTable(QueryFilterDto filters)
        {
            // Preparar la consulta inicial con Entity Framework
            IQueryable<T> query = _context.Set<T>();

            // Aplicar filtro por clave foránea si es necesario
            if (filters.ForeignKey != null && !string.IsNullOrEmpty(filters.NameForeignKey))
            {
                query = query.Where(i => EF.Property<int>(i, filters.NameForeignKey) == filters.ForeignKey);
            }

            // Aplicar filtros dinámicos si es necesario    
            if (!string.IsNullOrEmpty(filters.Filter))
            {
                query = (IQueryable<T>)PagedListDto<T>.ApplyDynamicFilters(query, filters);
            }

            // Ordenar los resultados si es necesario
            if (!string.IsNullOrEmpty(filters.ColumnOrder) && !string.IsNullOrEmpty(filters.DirectionOrder))
            {
                query = PagedListDto<T>.ApplyOrdering(query, filters);
            }

            IEnumerable<T> lstModel = await query.ToListAsync();

            List<D> lstDto = new List<D>();
            foreach (var item in lstModel)
            {
                D dto = _mapper.Map<D>(item);
                lstDto.Add(dto);
            }
            return lstDto;
        }

        /// <summary>
        /// Obtiene una entidad específica por su identificador.
        /// </summary>
        public override async Task<T> GetById(int id)
        {
            // Lógica para obtener un elemento por ID
            // Puedes implementar esto en clases concretas
            return await _context.Set<T>().FindAsync(id);
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
        /// Elimina una entidad por su identificador.
        /// </summary>
        public override async Task<int> Delete(int id)
        {
            int entity = await _context.Set<T>().Where(d => d.Id == id).ExecuteDeleteAsync();
            return entity;
        }
    }
}
