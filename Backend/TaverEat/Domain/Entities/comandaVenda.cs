namespace Domain.Entities;
public class ComandaVenda
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string? EntregaDir { get; set; }
    public DateTime Data { get; set; }
    public string Estat { get; set; }

    public ComandaVenda(Guid id, Guid clientId, string? entregaDir, DateTime data, string estat )
    {
        Id = id;
        ClientId = clientId;
        EntregaDir = entregaDir;
        Data = data;
        Estat = estat;
    }

}

