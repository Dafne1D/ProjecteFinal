namespace Domain.Entities;
public class Categoria
{
    public string Nom { get; set; }

    public Categoria(string nom)
    {
        Nom = nom;
    }
}