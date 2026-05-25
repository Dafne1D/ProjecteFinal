using System.Reflection.Metadata;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.DTO;
namespace Infrastructure.Mappers;

public static class ClientMapper
{
    public static Client ToDomain(ClientEntity entity)
        => new Client(
            entity.Id,
            entity.Nom,
            entity.Email,
            entity.Direccio,
            entity.Contrasenya, 
            entity.Role
    );
    public static ClientEntity ToEntity(Client client)
        => new ClientEntity
        {
            Id = client.Id,
            Nom = client.Nom,
            Email = client.Email,
            Direccio = client.Direccio,
            Contrasenya = client.Contrasenya,
            Role = client.Role
        };

       public static Client FromRequest(ClientRequest request)
        => new Client(
            Guid.NewGuid(),
            request.Nom,
            request.Email,
            request.Direccio,
            request.Contrasenya,
            "user"
        );
}