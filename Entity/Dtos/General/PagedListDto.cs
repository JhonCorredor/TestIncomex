using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Entity.Dtos.General
{
    /// <summary>
    /// Clase genérica para representar una lista paginada de elementos.
    /// </summary>
    /// <typeparam name="T">Tipo de los elementos que se almacenarán en la lista.</typeparam>
    public class PagedListDto<T>
    {
        /// <summary>
        /// Lista de elementos de la página actual.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Metadatos relacionados con la paginación.
        /// </summary>
        public MetaDataDto MetaData { get; set; }

        /// <summary>
        /// Constructor para inicializar una instancia de PagedListDto con los elementos proporcionados.
        /// </summary>
        /// <param name="items">Lista de elementos de la página.</param>
        /// <param name="count">Número total de elementos.</param>
        /// <param name="pageNumber">Número de la página actual.</param>
        /// <param name="pageSize">Tamaño de la página.</param>
        public PagedListDto(List<T> items, int count, int pageNumber = 1, int pageSize = 10)
        {
            Items = items;
            MetaData = new MetaDataDto
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                HasPreviousPage = pageNumber > 1,
                HasNextPage = pageNumber < (int)Math.Ceiling(count / (double)pageSize),
                NextPageUrl = pageNumber < (int)Math.Ceiling(count / (double)pageSize) ? $"?pageNumber={pageNumber + 1}&pageSize={pageSize}" : null,
                PreviousPageUrl = pageNumber > 1 ? $"?pageNumber={pageNumber - 1}&pageSize={pageSize}" : null
            };
        }

        /// <summary>
        /// Crea una instancia de PagedListDto aplicando paginación a la fuente de datos proporcionada.
        /// </summary>
        /// <param name="source">Fuente de datos original.</param>
        /// <param name="pageNumber">Número de la página solicitada.</param>
        /// <param name="pageSize">Tamaño de la página solicitada.</param>
        /// <returns>Una lista paginada de tipo PagedListDto.</returns>
        public static PagedListDto<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedListDto<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Aplica filtros dinámicos a una consulta de datos.
        /// </summary>
        /// <param name="query">Consulta original de los datos.</param>
        /// <param name="queryFilterDto">Filtros que se deben aplicar.</param>
        /// <returns>Una colección filtrada de elementos.</returns>
        public static IEnumerable<T> ApplyDynamicFilters(IEnumerable<T> query, QueryFilterDto queryFilterDto)
        {
            if (!string.IsNullOrEmpty(queryFilterDto.Filter) && !string.IsNullOrEmpty(queryFilterDto.ColumnFilter))
            {
                // Lógica de ejemplo para aplicar filtros dinámicos a propiedades de cadena
                query = query.Where(i => EF.Property<string>(i, queryFilterDto.ColumnFilter).Contains(queryFilterDto.Filter));
            }

            return query;
        }

        /// <summary>
        /// Aplica ordenación a la consulta según los parámetros especificados.
        /// </summary>
        /// <param name="query">Consulta original de los datos.</param>
        /// <param name="queryFilterDto">Parámetros de ordenación a aplicar.</param>
        /// <returns>Consulta ordenada.</returns>
        public static IOrderedQueryable<T> ApplyOrdering(IEnumerable<T> query, QueryFilterDto queryFilterDto)
        {
            if (!string.IsNullOrEmpty(queryFilterDto.ColumnOrder))
            {
                var queryIQueryable = query.AsQueryable();
                return OrderByProperty(queryIQueryable, queryFilterDto.ColumnOrder, queryFilterDto.DirectionOrder);
            }

            return query.AsQueryable() as IOrderedQueryable<T>;
        }

        /// <summary>
        /// Ordena la fuente de datos por una propiedad específica en una dirección especificada.
        /// </summary>
        /// <typeparam name="T">Tipo de los elementos en la fuente de datos.</typeparam>
        /// <param name="source">Fuente de datos original.</param>
        /// <param name="propertyName">Nombre de la propiedad por la cual se va a ordenar.</param>
        /// <param name="direction">Dirección de la ordenación ('asc' para ascendente o 'desc' para descendente).</param>
        /// <returns>Una consulta ordenada según la propiedad y dirección especificadas.</returns>
        public static IOrderedQueryable<T> OrderByProperty<T>(IQueryable<T> source, string propertyName, string direction)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException($"Property '{propertyName}' not found on type '{type.Name}'.");

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var resultExp = Expression.Call(
                typeof(Queryable),
                direction == "desc" ? "OrderByDescending" : "OrderBy",
                new[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExp)
            );

            return source.Provider.CreateQuery<T>(resultExp) as IOrderedQueryable<T>;
        }
    }
}
