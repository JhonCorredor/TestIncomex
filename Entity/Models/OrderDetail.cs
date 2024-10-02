namespace Entity.Models
{
    /// <summary>
    /// Detalles de un pedido específico.
    /// </summary>
    public class OrderDetail : BaseModel
    {
        public int OrderID { get; set; }

        /// <summary>
        /// Identificador del producto. Llave foránea relacionada con Product.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Precio unitario del producto en el pedido.
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Cantidad del producto en el pedido.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Descuento aplicado al producto en el pedido.
        /// </summary>
        public float Discount { get; set; }
    }
}