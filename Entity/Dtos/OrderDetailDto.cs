namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la entidad OrderDetail.
    /// </summary>
    public class OrderDetailDto
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
    }
}
