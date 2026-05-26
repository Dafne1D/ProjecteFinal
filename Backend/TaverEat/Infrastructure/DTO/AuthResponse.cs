namespace Infrastructure.DTO;

public record AuthResponse(string Token, UserAuthResponse User, string role);