using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(Guid id);
    IEnumerable<Product> GetByCategory(string categoryNom);
    IEnumerable<(Product product, string? imgUrl)> SearchWithImages(string query);
    IEnumerable<(Product product, string? imgUrl)> GetWithImagesByCategory(string categoryNom);
}
