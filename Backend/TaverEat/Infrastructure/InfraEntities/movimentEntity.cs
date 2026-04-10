namespace Infrastructure.InfraEntites;
public class MovimentEntity
{
    public Guid Id { get; set; }
    public required string CategoriaNom { get; set; }
    public Guid IngredientId { get; set; }
    public int Quantitat { get; set; }
    public DateTime Data { get; set; }
}
