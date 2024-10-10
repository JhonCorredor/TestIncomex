namespace Entity.Models
{

    /// <summary>
    /// Representa al proveedor de envío.
    /// </summary>
    public class Shipper : BaseModel
    {
        /// <summary>
        /// Identificador del pedido. Llave foránea relacionada con Order.
        /// </summary>
        public string CompanyName {  get; set; }
        
        /// <summary>
        /// Identificador del pedido. Llave foránea relacionada con Order.
        /// </summary>
        public string Phone {  get; set; }
    }
}
