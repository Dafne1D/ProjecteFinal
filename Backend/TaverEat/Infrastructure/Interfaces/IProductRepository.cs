using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IProductRepository
{
    List<Product> GetAll();
    List<Product> GetByCategoriaNom(string categoryNom);
    List<(Product product, string? imgUrl)> SearchProductWithImages(string query);
    List<(Product product, string? imgUrl)> GetProductsWithImagesByCategoriaNom(string categoryNom);
    void Insert(Product product);
    void Update(Product product);
    bool Delete(string nom);
}
