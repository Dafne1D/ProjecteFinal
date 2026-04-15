using Domain.Entities;

namespace TaverEat.Infrastructure.DTO;

public record ProductResponse(Guid Id, string Nom, string Descripcio, decimal Preu, string? ImgUrl) 
{
    // Guanyem CONTROL sobre com es fa la conversió
    public static ProductResponse FromProduct(Product product, string? imgUrl = null)   // Conversió d'entitat a response
    {
        return new ProductResponse(product.Id, product.Nom, product.Descripcio, product.Preu, imgUrl);
    }
}
