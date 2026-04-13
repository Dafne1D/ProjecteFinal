namespace Domain.Entities;

public class imgUrlEntity
{
    public required Guid Id { get; set; }
    public required string imgUrl { get; set; }
    public required Guid producte_id { get; set;}
}