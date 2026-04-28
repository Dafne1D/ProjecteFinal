using Domain.Entities;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public static class ProductMapper
{
    public static Product ToDomain(ProductEntity entity)
        => new Product
        {
            Id = entity.Id,
            Nom = entity.Nom,
            Descripcio = entity.Descripcio,
            Preu = entity.Preu,
            Categoria_nom = entity.CategoryNom
        };

    public static ProductEntity ToEntity(Product product)
        => new ProductEntity
        {
            Id = product.Id,
            Nom = product.Nom,
            Descripcio = product.Descripcio,
            Preu = product.Preu,
            CategoryNom = product.Categoria_nom
        };
}
