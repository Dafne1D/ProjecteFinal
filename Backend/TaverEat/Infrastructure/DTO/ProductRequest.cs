using Domain.Entities;

namespace TaverEat.Infrastructure.DTO;

public record ProductRequest(string Nom, string Descripcio, decimal Preu, string Categoria_nom) 
{
    public Product ToProduct(Guid id)   // Conversió a model
    {
        return new Product
        {
            Id = id,
            Nom = Nom,
            Descripcio = Descripcio,
            Preu = Preu,
            Categoria_nom = Categoria_nom
        };
    }
}
