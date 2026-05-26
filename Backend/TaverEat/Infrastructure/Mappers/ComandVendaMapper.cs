using Domain.Entities;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public static class ComandaVendaMapper
{
public static ComandaVenda ToDomain(ComandaVendaEntity entity)
        => new ComandaVenda(entity.Id, entity.ClientId, entity.EntregaDir, entity.Data, entity.Estat);

    public static ComandaVendaEntity ToEntity(ComandaVenda comanda)
        => new ComandaVendaEntity
        {
            Id = comanda.Id,
            ClientId = comanda.ClientId,
            EntregaDir = comanda.EntregaDir,
            Data = comanda.Data,
            Estat = comanda.Estat,
        };
}