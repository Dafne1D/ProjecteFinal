namespace Domain.Entities;
public class Product
{
    public required string Nom { get; set; }
    public required string Descripcio { get; set; }
    public decimal Preu { get; set; }
}