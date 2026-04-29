namespace Domain.Entities;
public class Categoria
{
    public required String Nom { get; set; }

    public Categoria(string nom)
    {
        Nom = nom;
    }
}