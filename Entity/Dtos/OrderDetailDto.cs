namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la entidad OrderDetail.
    /// </summary>
    public class OrderDetailDto : BaseDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
    }
}
