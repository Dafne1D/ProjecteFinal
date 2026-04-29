namespace Domain.Entities;
public class Product
{
    public Guid Id { get; set; }
    public string Nom { get; set; }
    public string Descripcio { get; set; }
    public decimal Preu { get; set; }
    public string Categoria_nom { get; set; }

    public Product(Guid id, string nom, string descripcio, decimal preu, string categoria_nom)
    {
        Id = id;
        Nom = nom;
        Descripcio = descripcio;
        Preu = preu;
        Categoria_nom = categoria_nom;
    }
}