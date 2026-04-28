using Microsoft.Data.SqlClient;
using Domain.Entities;
using API.Services;
using Infrastructure.Interfaces;
using Infrastructure.Mappers;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly TaverDBConnection _dbConn;

    public ProductRepository(TaverDBConnection dbConn)
    {
        _dbConn = dbConn;
    }

    public IEnumerable<Product> GetAll()
    {
        List<Product> products = new();
        _dbConn.Open();

        string sql = @"SELECT Id, Nom, Descripcio, Preu, Categoria_nom FROM producte";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var entity = new ProductEntity
            {
                Id = reader.GetGuid(0),
                Nom = reader.GetString(1),
                Descripcio = reader.GetString(2),
                Preu = reader.GetDecimal(3),
                CategoryNom = reader.GetString(4)
            };
            products.Add(ProductMapper.ToDomain(entity));
        }

        _dbConn.Close();
        return products;
    }

    public Product? GetById(Guid id)
    {
        _dbConn.Open();
        string sql = @"SELECT Id, Nom, Descripcio, Preu, Categoria_nom FROM producte WHERE Id = @Id";
        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = cmd.ExecuteReader();
        Product? product = null;

        if (reader.Read())
        {
            var entity = new ProductEntity
            {
                Id = reader.GetGuid(0),
                Nom = reader.GetString(1),
                Descripcio = reader.GetString(2),
                Preu = reader.GetDecimal(3),
                CategoryNom = reader.GetString(4)
            };
            product = ProductMapper.ToDomain(entity);
        }

        _dbConn.Close();
        return product;
    }

    public IEnumerable<Product> GetByCategory(string categoryNom)
    {
        List<Product> products = new();
        _dbConn.Open();
        string sql = @"SELECT Id, Nom, Descripcio, Preu, Categoria_nom FROM producte WHERE Categoria_nom = @CategoryNom";
        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@CategoryNom", categoryNom);

        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var entity = new ProductEntity
            {
                Id = reader.GetGuid(0),
                Nom = reader.GetString(1),
                Descripcio = reader.GetString(2),
                Preu = reader.GetDecimal(3),
                CategoryNom = reader.GetString(4)
            };
            products.Add(ProductMapper.ToDomain(entity));
        }

        _dbConn.Close();
        return products;
    }

    public IEnumerable<(Product product, string? imgUrl)> SearchWithImages(string query)
    {
        List<(Product, string?)> results = new();
        _dbConn.Open();
        string sql = @"
            SELECT product.Id, product.Nom, product.Descripcio, product.Preu, product.Categoria_nom, 
                   (SELECT TOP 1 img.Url FROM img_url img WHERE img.Producte_id = product.Id) as Url
            FROM producte product
            WHERE product.Nom LIKE @query OR product.Descripcio LIKE @query";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@query", "%" + query + "%");

        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var entity = new ProductEntity
            {
                Id = reader.GetGuid(0),
                Nom = reader.GetString(1),
                Descripcio = reader.GetString(2),
                Preu = reader.GetDecimal(3),
                CategoryNom = reader.GetString(4)
            };
            string? imgUrl = reader.IsDBNull(5) ? null : reader.GetString(5);
            results.Add((ProductMapper.ToDomain(entity), imgUrl));
        }

        _dbConn.Close();
        return results;
    }

    public IEnumerable<(Product product, string? imgUrl)> GetWithImagesByCategory(string categoryNom)
    {
        List<(Product, string?)> results = new();
        _dbConn.Open();
        string sql = @"
            SELECT product.Id, product.Nom, product.Descripcio, product.Preu, product.Categoria_nom, 
                   (SELECT TOP 1 img.Url FROM img_url img WHERE img.Producte_id = product.Id) as Url
            FROM producte product
            WHERE product.Categoria_nom = @CategoryNom";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@CategoryNom", categoryNom);

        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var entity = new ProductEntity
            {
                Id = reader.GetGuid(0),
                Nom = reader.GetString(1),
                Descripcio = reader.GetString(2),
                Preu = reader.GetDecimal(3),
                CategoryNom = reader.GetString(4)
            };
            string? imgUrl = reader.IsDBNull(5) ? null : reader.GetString(5);
            results.Add((ProductMapper.ToDomain(entity), imgUrl));
        }

        _dbConn.Close();
        return results;
    }
}
