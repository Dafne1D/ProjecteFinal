namespace Infrastructure.DTO;

public record UserAuthResponse(
    Guid Id,
    string Nom,
    string Email,
    string Role
);