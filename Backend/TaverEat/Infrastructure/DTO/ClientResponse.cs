using Domain.Entities;

namespace Infrastructure.DTO;

public record ClientResponse(Guid Id, string Nom, string Cognom, string Email, string Direccio)
{
    public static ClientResponse FromClient(Client client)
    {
        return new ClientResponse(client.Id, client.Nom, client.Cognom, client.Email, client.Direccio);
    }
}