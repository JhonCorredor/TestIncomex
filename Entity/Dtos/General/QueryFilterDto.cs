namespace Entity.Dtos.General
{
    /// <summary>
    /// DTO utilizado para proporcionar filtros avanzados para consultas de datos.
    /// </summary>
    public class QueryFilterDto : BasicQueryFilterDto
    {
        /// <summary>
        /// Valor adicional que puede ser utilizado para filtros específicos.
        /// </summary>
        public decimal? Extra { get; set; }

        /// <summary>
        /// Clave foránea que puede ser utilizada para filtrar los datos.
        /// </summary>
        public int? ForeignKey { get; set; }

        /// <summary>
        /// Nombre de la propiedad que actúa como clave foránea para realizar el filtrado.
        /// </summary>
        public string? NameForeignKey { get; set; }

        /// <summary>
        /// Fecha de inicio para filtrar datos dentro de un rango de fechas.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Fecha de fin para filtrar datos dentro de un rango de fechas.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Lista de claves foráneas para realizar filtros con múltiples valores.
        /// </summary>
        public List<int>? ForeignKeys { get; set; }
    }
}
