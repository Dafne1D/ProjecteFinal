using Microsoft.Data.SqlClient;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.InfraEntites;
using API.Services;
using Infrastructure.Mappers;

namespace Infrastructure.Repositories;

public class ProductADO : IProductRepository
{
    private readonly TaverDBConnection _dbConn;

    public ProductADO(TaverDBConnection dbConn)
    {
        _dbConn = dbConn;
    }

    public List<Product> GetAll()
    {
        List<Product> products = new();
        _dbConn.Open();

        string sql = "SELECT Id, Nom, Descripcio, Preu, Categoria_nom FROM producte";
        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            products.Add(MapProduct(reader));

        _dbConn.Close();
        return products;
    }

    public List<Product> GetByCategoriaNom(string categoriaNom)
    {
        List<Product> products = new();
        _dbConn.Open();

        string sql = "SELECT Id, Nom, Descripcio, Preu, Categoria_nom FROM producte WHERE Categoria_nom = @Categoria_nom";
        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Categoria_nom", categoriaNom);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            products.Add(MapProduct(reader));

        _dbConn.Close();
        return products;
    }

    public List<(Product product, string? imgUrl)> GetProductsWithImagesByCategoriaNom(string categoriaNom)
    {
        _dbConn.Open();
        string sql = @"
            SELECT p.Id, p.Nom, p.Descripcio, p.Preu, p.Categoria_nom,
                   (SELECT TOP 1 img.Url FROM img_url img WHERE img.Producte_id = p.Id) as Url
            FROM producte p
            WHERE p.Categoria_nom = @Categoria_nom";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Categoria_nom", categoriaNom);
        using SqlDataReader reader = cmd.ExecuteReader();

        List<(Product, string?)> results = new();
        while (reader.Read())
            results.Add((MapProduct(reader), reader.IsDBNull(5) ? null : reader.GetString(5)));

        _dbConn.Close();
        return results;
    }

    public List<(Product product, string? imgUrl)> SearchProductsWithImages(string query)
    {
        _dbConn.Open();
        string sql = @"
            SELECT p.Id, p.Nom, p.Descripcio, p.Preu, p.Categoria_nom,
                   (SELECT TOP 1 img.Url FROM img_url img WHERE img.Producte_id = p.Id) as Url
            FROM producte p
            WHERE p.Nom LIKE @query OR p.Descripcio LIKE @query";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@query", "%" + query + "%");
        using SqlDataReader reader = cmd.ExecuteReader();

        List<(Product, string?)> results = new();
        while (reader.Read())
            results.Add((MapProduct(reader), reader.IsDBNull(5) ? null : reader.GetString(5)));

        _dbConn.Close();
        return results;
    }

    public void Insert(Product product)
    {
        _dbConn.Open();
        string sql = @"INSERT INTO producte (Id, Nom, Descripcio, Preu, Categoria_nom)
                       VALUES (@Id, @Nom, @Descripcio, @Preu, @Categoria_nom)";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Id", product.Id == Guid.Empty ? Guid.NewGuid() : product.Id);
        cmd.Parameters.AddWithValue("@Nom", product.Nom);
        cmd.Parameters.AddWithValue("@Descripcio", product.Descripcio);
        cmd.Parameters.AddWithValue("@Preu", product.Preu);
        cmd.Parameters.AddWithValue("@Categoria_nom", product.Categoria_nom);
        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public void Update(Product product)
    {
        _dbConn.Open();
        string sql = @"UPDATE producte SET Descripcio = @Descripcio, Preu = @Preu, Categoria_nom = @Categoria_nom
                       WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", product.Nom);
        cmd.Parameters.AddWithValue("@Descripcio", product.Descripcio);
        cmd.Parameters.AddWithValue("@Preu", product.Preu);
        cmd.Parameters.AddWithValue("@Categoria_nom", product.Categoria_nom);
        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public bool Delete(string nom)
    {
        _dbConn.Open();
        string sql = "DELETE FROM producte WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", nom);
        int rows = cmd.ExecuteNonQuery();

        _dbConn.Close();
        return rows > 0;
    }

        private static Product MapProduct(SqlDataReader reader)
        {
            var entity = new ProductEntity
            {
                Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                Nom = reader.GetString(1),
                Descripcio = reader.GetString(2),
                Preu = reader.GetDecimal(3),
                CategoryNom = reader.GetString(4)
            };

            return ProductMapper.ToDomain(entity);
        }
}