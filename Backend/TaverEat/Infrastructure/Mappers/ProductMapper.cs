using Domain.Entities;
using Infrastructure.InfraEntites;

namespace Infrastructure.Mappers;

public static class ProductMapper
{
    public static Product ToDomain(ProductEntity entity)
        => new Product
        (
            entity.Id,
            entity.Nom,
            entity.Descripcio,
            entity.Preu,
            entity.CategoryNom
        );

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
