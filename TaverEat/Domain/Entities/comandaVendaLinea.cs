namespace Domain.Entities;

public class ComandaVendaLinea
{
    public required Product Product { get; set; }
    public int Quantitat { get; set; }
}