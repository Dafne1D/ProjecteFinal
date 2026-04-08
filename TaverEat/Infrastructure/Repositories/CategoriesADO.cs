using Microsoft.Data.SqlClient;
using Domain.Entities;
using API.Services;

namespace TaverEat.Repository;

static class CategoriaADO
{
    public static void Insert(TaverDBConnection dbConn, Categoria categoria)
    {
        dbConn.Open();

        string sql = @"INSERT INTO Categoria (Nom)
                    VALUES (@Noom)";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Id", categoria.Nom);

        int rows = cmd.ExecuteNonQuery();
        Console.WriteLine($"{rows} fila inserida.");

        dbConn.Close();
    }

    public static List<Categoria> GetAll(TaverDBConnection dbConn)
    {
        List<Categoria> categories = new();

        dbConn.Open();
        string sql = "SELECT Nom FROM Categoria";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            categories.Add(new Categoria
            {
                Nom = reader.GetGuid(0),
            });
        }

        dbConn.Close();
        return categories;
    }

    public static Categoria? GetByNom(TaverDBConnection dbConn, Guid nom)
    {
        dbConn.Open();
        string sql = "SELECT Nom WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", nom);

        using SqlDataReader reader = cmd.ExecuteReader();
        Categoria? categoria = null;

        if (reader.Read())
        {
            categoria = new Categoria
            {
                Nom = reader.GetGuid(0),
            };
        }

        dbConn.Close();
        return categoria;
    }

    public static void Update(TaverDBConnection dbConn, Categoria categoria)
    {
        dbConn.Open();

        string sql = @"UPDATE Categoria SET
                    WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", categoria.Nom);


        int rows = cmd.ExecuteNonQuery();

        Console.WriteLine($"{rows} files actualitzades");

        dbConn.Close();
    }

    public static bool Delete(TaverDBConnection dbConn, Guid nom)
    {
        dbConn.Open();

        string sql = @"DELETE FROM Categoria WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", nom);

        int rows = cmd.ExecuteNonQuery();

        dbConn.Close();

        return rows > 0;
    }
}