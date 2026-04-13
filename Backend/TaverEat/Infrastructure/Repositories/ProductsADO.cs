using Microsoft.Data.SqlClient;
using Domain.Entities;
using API.Services;

namespace TaverEat.Repository;

static class ProductADO
{
    public static void Insert(TaverDBConnection dbConn, Product product)
    {
        dbConn.Open();

        string sql = @"INSERT INTO producte (Nom, Descripcio, Preu, Categoria_nom)
                       VALUES (@Nom, @Descripcio, @Preu, @Categoria_nom)";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", product.Nom);
        cmd.Parameters.AddWithValue("@Descripcio", product.Descripcio);
        cmd.Parameters.AddWithValue("@Preu", product.Preu);
        cmd.Parameters.AddWithValue("@Categoria_nom", product.Categoria_nom);

        cmd.ExecuteNonQuery();
        dbConn.Close();
    }

    public static List<Product> GetAll(TaverDBConnection dbConn)
    {
        List<Product> products = new();

        dbConn.Open();

        string sql = @"SELECT Nom, Descripcio, Preu, Categoria_nom FROM producte";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product
            {
                Nom = reader.GetString(0),
                Descripcio = reader.GetString(1),
                Preu = reader.GetDecimal(2),
                Categoria_nom = reader.GetString(3)
            });
        }

        dbConn.Close();
        return products;
    }

    public static List<Product> GetByCategoriaNom(TaverDBConnection dbConn, string Categoria_nom)
    {
        dbConn.Open();

        string sql = @"SELECT Nom, Descripcio, Preu, Categoria_nom
                       FROM producte
                       WHERE Categoria_nom = @Categoria_nom";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Categoria_nom", Categoria_nom);

        using SqlDataReader reader = cmd.ExecuteReader();

        List<Product> products = new();

        while (reader.Read())
        {
            products.Add(new Product
            {
                Nom = reader.GetString(0),
                Descripcio = reader.GetString(1),
                Preu = reader.GetDecimal(2),
                Categoria_nom = reader.GetString(3)
            });
        }

        dbConn.Close();
        return products;
    }

    public static void Update(TaverDBConnection dbConn, Product product)
    {
        dbConn.Open();

        string sql = @"UPDATE producte SET
                       Descripcio = @Descripcio,
                       Preu = @Preu,
                       Categoria_nom = @Categoria_nom
                       WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", product.Nom);
        cmd.Parameters.AddWithValue("@Descripcio", product.Descripcio);
        cmd.Parameters.AddWithValue("@Preu", product.Preu);
        cmd.Parameters.AddWithValue("@Categoria_nom", product.Categoria_nom);
          cmd.ExecuteNonQuery();
        dbConn.Close();
    }

    public static bool Delete(TaverDBConnection dbConn, string nom)
    {
        dbConn.Open();

        string sql = @"DELETE FROM producte WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", nom);

        int rows = cmd.ExecuteNonQuery();

        dbConn.Close();
        return rows > 0;
    }
}