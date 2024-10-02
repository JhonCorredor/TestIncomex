namespace Entity.Models
{
    /// <summary>
    /// Representa un empleado de la compañía.
    /// </summary>
    public class Employee : BaseModel
    {
        /// <summary>
        /// Apellido del empleado.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Nombre del empleado.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Título del empleado.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Título de cortesía del empleado.
        /// </summary>
        public string TitleOfCourtesy { get; set; }

        /// <summary>
        /// Fecha de nacimiento del empleado.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Fecha de contratación del empleado.
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Dirección del empleado.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ciudad del empleado.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Región del empleado.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Código postal del empleado.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// País del empleado.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Teléfono del empleado.
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary>
        /// Extensión telefónica del empleado.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Foto del empleado.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Notas sobre el empleado.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Identificador del empleado al que reporta.
        /// </summary>
        public int? ReportsTo { get; set; }
    }
}
