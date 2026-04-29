namespace Infrastructure.InfraEntites;
public class ProductEntity
{
    public Guid Id { get; set; }
    public required string Nom { get; set; }
    public required string Descripcio { get; set; }
    public decimal Preu { get; set; }
    public required string CategoryNom { get; set; }
}