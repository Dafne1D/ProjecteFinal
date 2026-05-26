using Domain.Entities;

namespace Infrastructure.DTO;

public record ComandaVendaRequest(Guid ClientId, string EntregaDir, DateTime Data, string Estat) 
{
    public ComandaVenda ToProduct(Guid id)
    {
        return new ComandaVenda(id, ClientId, EntregaDir, Data, Estat);
    }
}
