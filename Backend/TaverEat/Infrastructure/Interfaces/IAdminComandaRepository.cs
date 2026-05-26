using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IAdminComandaRepository
{
    List<ComandaVenda> GetAll();
    List<ComandaVendaLinea> GetLineas(Guid comandaId);
    void UpdateEstat(Guid comandaId, string estat);
    ComandaVenda? GetById(Guid id);
}