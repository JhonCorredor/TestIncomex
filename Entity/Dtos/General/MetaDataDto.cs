namespace Entity.Dtos.General
{
    /// <summary>
    /// DTO utilizado para proporcionar metadatos relacionados con la paginación de los resultados de una consulta.
    /// </summary>
    public class MetaDataDto
    {
        /// <summary>
        /// Cantidad total de elementos en la colección consultada.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Tamaño de la página, indica cuántos registros se devuelven por página.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Página actual de los resultados que se está mostrando.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Número total de páginas calculadas a partir de <see cref="TotalCount"/> y <see cref="PageSize"/>.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Indica si hay una página siguiente disponible.
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Indica si hay una página anterior disponible.
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// URL de la página siguiente, si existe.
        /// </summary>
        public string NextPageUrl { get; set; } = null!;

        /// <summary>
        /// URL de la página anterior, si existe.
        /// </summary>
        public string PreviousPageUrl { get; set; } = null!;
    }
}
