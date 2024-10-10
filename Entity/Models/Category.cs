namespace Entity.Models
{
    /// <summary>
    /// Representa una categoría de productos.
    /// </summary>
    public class Category : BaseModel
    {
        /// <summary>
        /// Nombre de la categoría.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Descripción de la categoría.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Imagen de la categoría.
        /// </summary>
        public byte[] Picture { get; set; }

    }
}
