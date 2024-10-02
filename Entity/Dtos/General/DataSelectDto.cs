namespace Entity.Dtos.General
{
    /// <summary>
    /// DTO utilizado para representar una selección simplificada de datos en listas desplegables u opciones de selección.
    /// </summary>
    public class DataSelectDto
    {
        /// <summary>
        /// Identificador único del elemento.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Texto que se mostrará en las interfaces de usuario para representar este elemento.
        /// </summary>
        public string TextoMostrar { get; set; } = null!;
    }
}
