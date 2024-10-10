namespace Entity.Dtos.General
{
    /// <summary>
    /// Clase utilizada para estandarizar las respuestas de la API.
    /// </summary>
    /// <typeparam name="T">Tipo de datos que se devuelven en la respuesta.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indica si la petición fue exitosa (True) o falló (False).
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Mensaje informativo sobre la respuesta de la petición.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Datos devueltos en la respuesta, puede ser una entidad o una lista de entidades.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Constructor vacío que permite establecer valores por defecto.
        /// </summary>
        public ApiResponse() { }

        /// <summary>
        /// Constructor que inicializa una respuesta con datos seleccionados.
        /// </summary>
        /// <param name="data">Lista de objetos `BaseDto` a devolver.</param>
        public ApiResponse(IEnumerable<BaseDto> data)
        {
            Status = true;
            Message = "Ok";
            Data = default;
        }

        /// <summary>
        /// Constructor que inicializa una respuesta con los datos proporcionados.
        /// </summary>
        /// <param name="data">Datos que serán devueltos como parte de la respuesta.</param>
        /// <param name="status">Estado de la respuesta, `true` para éxito, `false` para error.</param>
        /// <param name="message">Mensaje explicativo sobre la respuesta.</param>
        /// <param name="meta">Información adicional (como paginación).</param>
        public ApiResponse(T data, bool status, string message)
        {
            Data = data;
            Status = status;
            Message = message;
        }
    }
}
