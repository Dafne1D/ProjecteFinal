namespace Infrastructure.Entities;

public class ComandaVendaEntity
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public DateTime Data { get; set; }
    public required string Estat { get; set; }
}