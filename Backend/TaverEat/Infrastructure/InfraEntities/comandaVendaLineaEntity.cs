namespace Infrastructure.Entities;

public class ComandaVendaLineaEntity
{
    public Guid Id { get; set; }
    public Guid ComandaId { get; set; }
    public Guid ProducteId { get; set; }
    public int Quantitat { get; set; }
}