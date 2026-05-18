namespace Infrastructure.DTO;

public record ComandaLineaResponse(
    Guid ProducteId,
    string Nom,
    decimal Preu,
    int Quantitat
);

public record ComandaDetallResponse(
    Guid ComandaId,
    List<ComandaLineaResponse> Productes,
    decimal Total
);