namespace Domain.Entities;
public class Product
{
    public Guid Id { get; set; }
    public required string Nom { get; set; }
    public required string Descripcio { get; set; }
    public decimal Preu { get; set; }
    public required string Categoria_nom { get; set; }
}