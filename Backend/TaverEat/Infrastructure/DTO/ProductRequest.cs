using Domain.Entities;

namespace Infrastructure.DTO;

public record ProductRequest(string Nom, string Descripcio, decimal Preu, string Categoria_nom) 
{
    public Product ToProduct(Guid id)
    {
        return new Product(id, Nom, Descripcio, Preu, Categoria_nom);
    }
}
