namespace Entity.Models
{
    /// <summary>
    /// Representa un pedido realizado por un cliente.
    /// </summary>
    public class Order : BaseModel
    {
        /// <summary>
        /// Identificador del cliente. Llave foránea relacionada con Customer.
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Identificador del empleado. Llave foránea relacionada con Employee.
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Determina qué proveedor de envío se utilizó para un pedido en particular. Llave foránea relacionada con Shipper.
        /// </summary>
        public int ShipVia { get; set; }
        
        /// <summary>
        /// Fecha del pedido.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Fecha requerida para el pedido.
        /// </summary>
        public DateTime RequiredDate { get; set; }

        /// <summary>
        /// Fecha en la que el pedido fue enviado.
        /// </summary>
        public DateTime? ShippedDate { get; set; }

        /// <summary>
        /// Costo del envío.
        /// </summary>
        public decimal Freight { get; set; }

        /// <summary>
        /// Dirección a la cual se envía el pedido.
        /// </summary>
        public string ShipAddress { get; set; }

        /// <summary>
        /// Ciudad a la cual se envía el pedido.
        /// </summary>
        public string ShipCity { get; set; }

        /// <summary>
        /// Región de destino del pedido.
        /// </summary>
        public string ShipRegion { get; set; }

        /// <summary>
        /// Código postal de destino del pedido.
        /// </summary>
        public int ShipPostalCode { get; set; }

        /// <summary>
        /// País de destino del pedido.
        /// </summary>
        public string ShipCountry { get; set; }
    }
}
