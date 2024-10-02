namespace Entity.Dtos.General
{
    /// <summary>
    /// DTO utilizado para definir los filtros y parámetros de paginación básicos en consultas.
    /// </summary>
    public class BasicQueryFilterDto
    {
        /// <summary>
        /// Tamaño de la página, define cuántos registros deben ser devueltos por página.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Número de la página que se desea obtener, utilizado para la paginación.
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Filtro general de búsqueda, se utiliza para buscar registros que coincidan con el valor especificado.
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// Nombre de la columna que se desea filtrar.
        /// </summary>
        public string? ColumnFilter { get; set; }

        /// <summary>
        /// Nombre de la columna por la cual se desea ordenar los resultados.
        /// </summary>
        public string? ColumnOrder { get; set; }

        /// <summary>
        /// Dirección del orden para los resultados: "asc" para ascendente o "desc" para descendente.
        /// </summary>
        public string? DirectionOrder { get; set; }
    }
}
