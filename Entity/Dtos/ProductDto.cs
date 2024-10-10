namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la entidad Product.
    /// </summary>
    public class ProductDto : BaseDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public byte[]? PictureCategory { get; set; }
    }
}
