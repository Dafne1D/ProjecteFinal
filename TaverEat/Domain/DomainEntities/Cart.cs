namespace Domain.Entities;

public class Cart
{
    public List<CartLinea> Linies { get; set; } = new List<CartLinea>();

    // Calcular total buissnes logica
    public decimal GetTotal()
    {
        decimal total = 0;
        foreach (var linea in Linies)
        {
            total += linea.Product.Preu * linea.Quantitat;
        }
        return total;
    }
}
