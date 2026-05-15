using Domain.Entities;
using Infrastructure.Interfaces;


namespace API.Services;

public class ComandaVendaService
{
    private readonly IComandaVendaRepository _repo;

    public ComandaVendaService(IComandaVendaRepository repo)
    {
        _repo = repo;
    }

    public ComandaVenda GetOrCreate(Guid clientId)
    {
        var comanda = _repo.GetComandaActivaByClient(clientId);

        if (comanda is null)
        {
            comanda = _repo.CreateComanda(clientId);
        }

        return comanda;
    }

    public void AddProduct(Guid clientId, Guid producteId)
    {
        var comanda = GetOrCreate(clientId);

        var linea = _repo.GetLinea(comanda.Id, producteId);

        if (linea is null)
        {
            linea = new ComandaVendaLinea(
                Guid.NewGuid(),
                producteId,
                comanda.Id,
                1
            );

            _repo.AddLinea(linea);
        }
        else
        {
            linea.Quantitat++;

            _repo.UpdateLinea(linea);
        }
    }
}