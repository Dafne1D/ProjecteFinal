using System.Reflection.Metadata;
using Domain.Entities;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public static class ClientMapper
{
    public static Client ToDomain(ClientEntity entity)
        => new Client(entity.Id, entity.Nom, entity.Cognom, entity.Email, entity.Direccio, entity.Contrasenya);
    public static ClientEntity ToEntity(Client client)
        => new ClientEntity
        {
            Id = client.Id,
            Nom = client.Nom,
            Cognom = client.Cognom,
            Email = client.Email,
            Direccio = client.Direccio,
            Contrasenya = client.Contrasenya
        };
}