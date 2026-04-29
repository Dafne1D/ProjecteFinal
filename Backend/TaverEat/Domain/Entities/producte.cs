namespace Domain.Entities;
public class Product
{
    public Guid Id { get; set; }
    public required string Nom { get; set; }
    public required string Descripcio { get; set; }
    public decimal Preu { get; set; }
    public required string Categoria_nom { get; set; }

    public Product(Guid id, string nom, string descripcio, decimal preu, string categoria_nom)
    {
        Id = id;
        Nom = nom;
        Descripcio = descripcio;
        Preu = preu;
        Categoria_nom = categoria_nom;
    }
}