using Domain.Entities;

public class Moviment
{
    public required string CategoriaNom { get; set; }
    public required Ingredient Ingredient { get; set; }
    public int Quantitat { get; set; }
    public DateTime Data { get; set; }
}