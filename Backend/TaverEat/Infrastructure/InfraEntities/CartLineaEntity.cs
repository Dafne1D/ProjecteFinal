namespace Infrastructure.Entities;

public class CartLineaEntity
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantitat { get; set; }
}
