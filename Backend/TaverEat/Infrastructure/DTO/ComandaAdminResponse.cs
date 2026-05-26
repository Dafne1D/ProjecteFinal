namespace Infrastructure.DTO;

public class ComandaAdminResponse
{
    public Guid Id { get; set; }
    public string Estat { get; set; }
    public DateTime Data { get; set; }

    // cuinant
    public List<LiniaComandaDto>? Productes { get; set; }

    // repartiment
    public string? Direccio { get; set; }
}

public class LiniaComandaDto
{
    public string NomProducte { get; set; }
    public int Quantitat { get; set; }
}