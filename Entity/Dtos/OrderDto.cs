namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la entidad Order.
    /// </summary>
    public class OrderDto : BaseDto
    {
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; } = string.Empty;
        public string ShipAddress { get; set; } = string.Empty;
        public string ShipCity { get; set; } = string.Empty;
        public string? ShipRegion { get; set; }
        public string ShipPostalCode { get; set; } = string.Empty;
        public string ShipCountry { get; set; } = string.Empty;
    }
}
