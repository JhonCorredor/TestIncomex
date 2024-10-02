namespace Entity.Dtos
{
    /// <summary>
    /// Clase base abstracta para los DTOs de entidades que tienen información de contacto.
    /// </summary>
    public abstract class BaseModelContactDto : BaseDto
    {
        public string CompanyName { get; set; } = string.Empty;      
        public string ContactName { get; set; } = string.Empty;      
        public string ContactTitle { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Region { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Fax { get; set; }
    }
}
