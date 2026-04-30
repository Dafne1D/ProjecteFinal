namespace Domain.Entities;

public class ComandaVendaLinea
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ComandaId { get; set; }
    public int Quantitat { get; set; }

    public ComandaVendaLinea(Guid id, Guid productId, Guid comandaId, int quantitat)
    {
        Id = id;
        ProductId = productId;
        ComandaId = comandaId;
        Quantitat = quantitat;
    }
}