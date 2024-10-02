namespace Entity.Models
{
    /// <summary>
    /// Representa un producto suministrado por un proveedor.
    /// </summary>
    public class Product : BaseModel
    {
        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Identificador del proveedor. Llave foránea relacionada con Supplier.
        /// </summary>
        public int SupplierID { get; set; }

        /// <summary>
        /// Identificador de la categoría. Llave foránea relacionada con Category.
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Cantidad de producto por unidad.
        /// </summary>
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// Precio unitario del producto.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Unidades disponibles en stock.
        /// </summary>
        public int UnitsInStock { get; set; }

        /// <summary>
        /// Unidades en orden.
        /// </summary>
        public int UnitsOnOrder { get; set; }

        /// <summary>
        /// Nivel de reordenamiento del producto.
        /// </summary>
        public int ReorderLevel { get; set; }

        /// <summary>
        /// Indicador de si el producto está descontinuado.
        /// </summary>
        public bool Discontinued { get; set; }
    }
}
