namespace Entity.Dtos
{
    //// <summary>
    /// DTO para la entidad Supplier, basada en BaseModelContactDto.
    /// </summary>
    internal class SupplierDto : BaseModelContactDto
    {
        public string? HomePage { get; set; }
    }
}
