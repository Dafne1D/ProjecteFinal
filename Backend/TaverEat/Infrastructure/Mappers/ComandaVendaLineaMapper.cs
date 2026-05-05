using Domain.Entities;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public static class ComandaVendaLineaMapper
{
    public static ComandaVendaLinea ToDomain(ComandaVendaLineaEntity entity)
        => new ComandaVendaLinea(entity.Id, entity.ComandaId, entity.ProducteId, entity.Quantitat);

    public static ComandaVendaLineaEntity ToEntity(ComandaVendaLinea comanda)
        => new ComandaVendaLineaEntity
        {
            Id = comanda.Id,
            ComandaId = comanda.ComandaId,
            ProducteId = comanda.ProducteId,
            Quantitat = comanda.Quantitat
        };
}