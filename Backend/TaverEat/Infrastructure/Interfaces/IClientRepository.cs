using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IClientRepository
{
    List<Client> GetAll();
    Client? GetById(Guid id);
    Client? GetByEmail(string email);
    void Insert(Client client);
    void Update(Client client);
    bool Delete(Guid id);
}