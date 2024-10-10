namespace Entity.Models
{
    /// <summary>
    /// Clase base abstracta para los modelos de entidades.
    /// Define las propiedades comunes que serán heredadas por otras entidades.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Identificador único de la entidad.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Indica si la entidad está activa o ha sido desactivada lógicamente.
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Fecha y hora en la que se creó la entidad.
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Fecha y hora en la que se actualizó la entidad por última vez.
        /// Es nulo si nunca se ha actualizado.
        /// </summary>
        public DateTime? UpdateAt { get; set; }

        /// <summary>
        /// Fecha y hora en la que se eliminó lógicamente la entidad.
        /// Es nulo si la entidad no ha sido eliminada.
        /// </summary>
        public DateTime? DeletedAt { get; set; }
    }
}
