using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IComandaVendaRepository
{
    // Buscar si hi ha una comanda activa
    ComandaVenda? GetComandaActivaByClient(Guid clientId);
    ComandaVenda CreateComanda(Guid clientId);

    ComandaVendaLinea? GetLinea(Guid comandaId, Guid ProducteId);
}