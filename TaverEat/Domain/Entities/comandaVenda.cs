namespace Domain.Entities;
public class ComandaVenda
{
    public DateTime Data { get; set; }
    public required string Estat { get; set; }

    public required Client Client { get; set; }
    public required List<ComandaVendaLinea> Linies { get; set; }
}