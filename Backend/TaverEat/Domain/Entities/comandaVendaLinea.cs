namespace Domain.Entities;

public class ComandaVendaLinea
{
    public Guid Id { get; set; }
    public Guid ProducteId { get; set; }
    public Guid ComandaId { get; set; }
    public int Quantitat { get; set; }

    public ComandaVendaLinea(Guid id, Guid producteId, Guid comandaId, int quantitat)
    {
        Id = id;
        ProducteId = producteId;
        ComandaId = comandaId;
        Quantitat = quantitat;  
    }
}