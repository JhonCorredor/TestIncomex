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
        public override async Task<IEnumerable<D>> GetAll(PaginationDto pagination)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>().Where(e => e.DeletedAt == null);

                if (pagination.PageNumber != 0 && pagination.PageSize != 0)
                {
                    int skip = (pagination.PageNumber.GetValueOrDefault(1) - 1) * pagination.PageSize.GetValueOrDefault(10);
                    query = query.Skip(skip).Take(pagination.PageSize.GetValueOrDefault(10));
                }

                var lstModel = await query.ToListAsync();

                List<D> lstDto = lstModel.Select(item => _mapper.Map<D>(item)).ToList();

                return lstDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recuperar datos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene una entidad por su identificador único.
        /// </summary>
        public override async Task<D> GetById(int id)
        {

            T Dto = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.DeletedAt == null);
            return _mapper.Map<D>(Dto);
        }

        /// <summary>
        /// Guarda una nueva entidad en la base de datos.
        /// </summary>
        public override async Task<T> Save(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al guardar la entidad.", ex);
            }
        }

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        public override async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "La entidad no puede ser nula.");
            }

            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException("La entidad que se está actualizando ha sido modificada por otro usuario.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Ocurrió un error al actualizar la entidad.", ex);
            }
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

            // Establecer la fecha de eliminación
            entity.DeletedAt = DateTime.UtcNow;

            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Ocurrió un error al intentar eliminar la entidad.", ex);
            }
        }

    }
}
