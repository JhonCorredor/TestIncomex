namespace Entity.Models
{
    /// <summary>
    /// Clase base para los modelos de entidades que tienen información de contacto.
    /// Define las propiedades comunes que serán heredadas por entidades como Proveedor y Cliente.
    /// </summary>
    public class BaseModelContact : BaseModel
    {
        /// <summary>
        /// Nombre de la compañía.
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del contacto de la compañía.
        /// </summary>
        public string ContactName { get; set; } = string.Empty;

        /// <summary>
        /// Título del contacto de la compañía.
        /// </summary>
        public string ContactTitle { get; set; } = string.Empty;

        /// <summary>
        /// Dirección física de la compañía.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Ciudad donde se encuentra la compañía.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Región donde se encuentra la compañía, puede ser nula si no aplica.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Código postal de la dirección de la compañía.
        /// Puede incluir caracteres alfanuméricos dependiendo del país.
        /// </summary>
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// País donde se encuentra la compañía.
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Teléfono de contacto de la compañía.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Número de fax de la compañía, puede ser nulo si no aplica.
        /// </summary>
        public string? Fax { get; set; }
    }
}
