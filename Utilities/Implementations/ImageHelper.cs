namespace Utilities.Implementations
{
    /// <summary>
    /// Clase de utilidad para realizar operaciones relacionadas con la manipulación de imágenes.
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Convierte una cadena en formato Base64 a un arreglo de bytes.
        /// </summary>
        /// <param name="base64String">La cadena en formato Base64 que se desea convertir.</param>
        /// <returns>Un arreglo de bytes (<see cref="byte[]"/>) que representa la imagen decodificada.</returns>
        /// <exception cref="FormatException">
        /// Se produce cuando la cadena proporcionada no está en el formato correcto de Base64.
        /// </exception>
        public static byte[] ConvertBase64ToBytes(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                throw new ArgumentException("El parámetro base64String no puede ser nulo o vacío.", nameof(base64String));
            }

            try
            {
                return Convert.FromBase64String(base64String);
            }
            catch (FormatException ex)
            {
                throw new FormatException("La cadena proporcionada no tiene un formato válido de Base64.", ex);
            }
        }

        /// <summary>
        /// Convierte un arreglo de bytes a una cadena en formato Base64.
        /// </summary>
        /// <param name="bytes">El arreglo de bytes que se desea convertir.</param>
        /// <returns>Una cadena en formato Base64 que representa los datos del arreglo de bytes.</returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce cuando el arreglo de bytes proporcionado es nulo.
        /// </exception>
        public static string ConvertBytesToBase64(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes), "El parámetro bytes no puede ser nulo.");
            }

            return Convert.ToBase64String(bytes);
        }
    }
}