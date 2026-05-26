using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IComandaVendaRepository
{
    ComandaVenda? GetComandaActivaByClient(Guid clientId);

    ComandaVenda CreateComanda(Guid clientId);

    ComandaVendaLinea? GetLinea(Guid comandaId, Guid producteId);

    void AddLinea(ComandaVendaLinea linea);

    void UpdateLinea(ComandaVendaLinea linea);

    void DeleteLinea(Guid lineaId);

    List<(ComandaVendaLinea linea, Product producte)>
        GetLineasWithProducte(Guid comandaId);

    void ConfirmarComanda(Guid comandaId);

    List<ComandaVenda> GetAll();

    void UpdateEstat(Guid comandaId, string estat);
    void SetEntregaDir(Guid comandaId, string direccioId);
    
}