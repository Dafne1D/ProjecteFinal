using Domain.Entities;

namespace Infrastructure.DTO;

public record ComandaVendaResponse(Guid Id, Guid ClientId, Guid EntregaDir, DateTime Data, string Estat) 
{
    public static ComandaVendaResponse FromComandaVenda(ComandaVenda comanda)  
    {
        return new ComandaVendaResponse(comanda.Id, comanda.ClientId, comanda.EntregaDir, comanda.Data, comanda.Estat);
    }
}
