using Domain.Entities;
using Infrastructure.InfraEntites;

namespace Infrastructure.Mappers;

public static class CategoriaMapper
{
    public static Categoria ToDomain(CategoriaEntity entity)
        => new Categoria(entity.Nom);

    public static CategoriaEntity ToEntity(Categoria categoria)
        => new CategoriaEntity { Nom = categoria.Nom };
}