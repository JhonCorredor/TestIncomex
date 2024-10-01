namespace Entity.Dtos
{
    public class QueryFilterDto : BasicQueryFilterDto
    {
        public decimal? Extra { get; set; }
        public int? ForeignKey { get; set; }
        public string? NameForeignKey { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int[]? ForeignKeys { get; set; }

    }
}
