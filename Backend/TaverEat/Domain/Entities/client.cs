namespace Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = "";
    public string Email { get; set; } = "";
    public string Direccio { get; set; } = "";
    public string Contrasenya { get; set; } = "";

    public Client(Guid id, string nom, string email, string direccio, string contrasenya)
    {
        Id = id;
        Nom = nom;
        Email = email;
        Direccio = direccio;
        Contrasenya = contrasenya;
    }
}