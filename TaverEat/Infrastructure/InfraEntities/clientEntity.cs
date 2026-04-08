namespace Infrastructure.InfraEntites;

public class ClientEntity
{
    public Guid Id { get; set; }
    public required string Nom { get; set; }
    public required string Cognom { get; set; }
    public required string Email { get; set; }
    public required string Contrasenya { get; set; }
}