using Domain.Entities;

namespace Infrastructure.DTO;

public record ClientRequest(string Nom, string Cognom, string Email, string Direccio, string Contrasenya)
{
    public Client ToClient()
    {
        return new Client(Guid.NewGuid(), Nom, Cognom, Email, Direccio, Contrasenya);
    }
}