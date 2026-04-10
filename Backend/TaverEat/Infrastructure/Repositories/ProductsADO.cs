using Microsoft.Data.SqlClient;
using Domain.Entities;
using API.Services;

namespace TaverEat.Repository;

static class ProductADO
{
    public static void Insert(TaverDBConnection dbConn, Product product)
    {
        dbConn.Open();

        string sql = @"INSERT INTO Product (Nom, Descripcio, Preu, CategoryNom)
                    VALUES (@Nom, @Descripcio, @Preu, @CategoryNom)";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", product.Nom);
        cmd.Parameters.AddWithValue("@Descripcio", product.Descripcio);
        cmd.Parameters.AddWithValue("@Preu", product.Preu);
        cmd.Parameters.AddWithValue("@CategoryNom", product.CategoryNom);

        int rows = cmd.ExecuteNonQuery();
        Console.WriteLine($"{rows} fila inserida.");

        dbConn.Close();
    }

    public static List<Product> GetAll(TaverDBConnection dbConn)
    {
        List<Product> products = new();

        dbConn.Open();
        string sql = "SELECT Nom FROM Product";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product
            {
                Nom = reader.GetString(0),
                Descripcio = reader.GetString(1),
                Preu = reader.GetDecimal(2),
                CategoryNom = reader.GetString(3)
            });
        }

        dbConn.Close();
        return products;
    }

public static List<Product> GetByCategoryNom(TaverDBConnection dbConn, string categoryNom)
{
    dbConn.Open();

    string sql = @"SELECT Nom, Descripcio, Preu
        FROM Products
        WHERE CategoryNom = @CategoryNom";

    using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
    cmd.Parameters.AddWithValue("@CategoryNom", categoryNom);

    using SqlDataReader reader = cmd.ExecuteReader();

    List<Product> products = new List<Product>();

    while (reader.Read())
    {
        products.Add(new Product
        {
            Nom = reader.GetString(0),
            Descripcio = reader.GetString(1),
            Preu = reader.GetDecimal(2),
            CategoryNom = reader.GetString(3)
        });
    }

    dbConn.Close();
    return products;
}
    public static void Update(TaverDBConnection dbConn, Product product)
    {
        dbConn.Open();

        string sql = @"UPDATE Product SET
                    Nom = @Nom,
                    Descripcio = @Descripcio,
                    Preu = @Preu,
                    CategoryNom = @CategoryNom
                    WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", product.Nom);
        cmd.Parameters.AddWithValue("@Descripcio", product.Descripcio);
        cmd.Parameters.AddWithValue("@Preu", product.Preu);
        cmd.Parameters.AddWithValue("CategoryNom", product.CategoryNom);


        int rows = cmd.ExecuteNonQuery();

        Console.WriteLine($"{rows} files actualitzades");

        dbConn.Close();
    }

    public static bool Delete(TaverDBConnection dbConn, string Nom)
    {
        dbConn.Open();

        string sql = @"DELETE FROM Product WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", Nom);

        int rows = cmd.ExecuteNonQuery();

        dbConn.Close();

        return rows > 0;
    }
}