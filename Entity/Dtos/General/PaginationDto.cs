namespace Entity.Dtos.General
{
    /// <summary>
    /// DTO utilizado para definir los filtros y parámetros de paginación básicos en consultas.
    /// </summary>
    public class PaginationDto
    {
        /// <summary>
        /// Tamaño de la página, define cuántos registros deben ser devueltos por página.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Número de la página que se desea obtener, utilizado para la paginación.
        /// </summary>
        public int? PageNumber { get; set; }
    }
}
