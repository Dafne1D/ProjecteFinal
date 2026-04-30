using Domain.Entities;
using Infrastructure.InfraEntites;

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
            Data = comanda.Data,
            Estat = comanda.Estat,
            EntregaDir = comanda.EntregaDir
        };
}