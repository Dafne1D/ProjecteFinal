namespace Domain.Entities;
public class ComandaVenda
{
    public DateTime Data { get; set; }
    public string Estat { get; set; }
    public string entrega_dir { get; set; } 
    public Client Client { get; set; }
    public List<ComandaVendaLinea> Linies { get; set; }
}