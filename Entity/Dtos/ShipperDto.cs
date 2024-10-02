namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la entidad Shipper.
    /// </summary>
    public class ShipperDto : BaseDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
