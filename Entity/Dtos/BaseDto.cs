namespace Entity.Dtos
{
    /// <summary>
    /// Clase base para los Data Transfer Objects (DTO).
    /// Define las propiedades comunes que serán heredadas por los DTOs de otras entidades.
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// Identificador único del DTO.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Indica si la entidad está activa o ha sido desactivada lógicamente.
        /// </summary>
        public bool ? Activo { get; set; }

        /// <summary>
        /// Fecha y hora en la que se creó la entidad.
        /// </summary>
        public DateTime ? CreateAt { get; set; }
    }
}
