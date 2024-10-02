namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la entidad Category.
    /// </summary>
    public class CategoryDto : BaseDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
    }
}
