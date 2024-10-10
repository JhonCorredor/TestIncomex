namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la entidad Order.
    /// </summary>
    public class OrderDto : BaseDto
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; } = string.Empty;
        public string ShipAddress { get; set; } = string.Empty;
        public string ShipCity { get; set; } = string.Empty;
        public string? ShipRegion { get; set; }
        public int ShipPostalCode { get; set; } = 0;
        public string ShipCountry { get; set; } = string.Empty;
    }
}
