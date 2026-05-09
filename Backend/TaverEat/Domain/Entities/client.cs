namespace Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = "";
    public string Cognom { get; set; } = "";
    public string Direccio { get; set; } = "";
    public string Contrasenya { get; set; } = "";

    public Client(Guid id, string nom, string cognom, string direccio, string contrasenya)
    {
        Id = id;
        Nom = nom;
        Cognom = cognom;
        Direccio = direccio;
        Contrasenya = contrasenya;
    }
}