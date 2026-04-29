using Infrastructure.DTO;

namespace Application.Interfaces;

public interface IProductService
{
    IEnumerable<ProductResponse> GetAllProducts();
    IEnumerable<ProductResponse> GetProductsByCategory(string categoryNom);
    IEnumerable<ProductResponse> SearchProducts(string query);
}
