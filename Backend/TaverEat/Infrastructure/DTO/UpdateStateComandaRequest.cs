using System.Text.Json.Serialization;

namespace Infrastructure.DTO;
public record UpdateEstatComandaRequest(
    [property: JsonPropertyName("estat")] string Estat
);