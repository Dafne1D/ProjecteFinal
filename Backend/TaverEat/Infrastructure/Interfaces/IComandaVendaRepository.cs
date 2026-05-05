using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IComandaVendaRepository
{
    // Buscar si hi ha una comanda activa
    ComandaVenda? GetComandaActivaByClient(Guid clientId);
    ComandaVenda CreateComandaVenda(Guid clientId);

    ComandaVendaLinea? GetLinea(Guid comandaId);
}