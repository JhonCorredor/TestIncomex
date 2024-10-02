namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la entidad Product.
    /// </summary>
    public class ProductDto : BaseDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
