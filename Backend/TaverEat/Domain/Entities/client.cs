namespace Domain.Entities;
public class Client
{
    public Guid Id { get; }
    public string Nom { get; }
    public string Email { get; }
    public string Direccio { get; }
    public string Contrasenya { get; }

    public Client(Guid id, string nom, string email, string direccio, string contrasenya)
    {
        Id = id;
        Nom = nom;
        Email = email;
        Direccio = direccio;
        Contrasenya = contrasenya;
    }
}