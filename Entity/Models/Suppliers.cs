namespace Entity.Models
{
    /// <summary>
    /// Representa un proveedor en el sistema, contiene información detallada de la empresa y del contacto.
    /// </summary>
    public class Suppliers : BaseModelContact
    {
        /// <summary>
        /// Página de inicio del proveedor, puede ser nula si no aplica.
        /// </summary>
        public string? HomePage { get; set; }
    }
}
