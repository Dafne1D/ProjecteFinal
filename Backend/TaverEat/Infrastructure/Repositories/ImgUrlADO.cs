using Microsoft.Data.SqlClient;
using Domain.Entities;
using API.Services;

namespace TaverEat.Repository;

static class ImgUrlADO
{
    public static void Insert(TaverDBConnection dbConn, imgUrl url)
    {
        dbConn.Open();

        string sql = @"INSERT INTO img_url (Url, Producte_id)
                    VALUES (@Url, @Producte_id)";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("Url", url.Url);
        cmd.Parameters.AddWithValue("Producte_id", url.Producte_id);


        int rows = cmd.ExecuteNonQuery();
        Console.WriteLine($"{rows} fila inserida.");

        dbConn.Close();
    }
    public static List<imgUrl> GetByProductId(TaverDBConnection dbConn, Guid producte_Id)
    {
        List<imgUrl> images = new();

        dbConn.Open();

        string sql = @"SELECT url, producte_id
                       FROM img_url
                       WHERE producte_id = @ProducteId";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@ProducteId", producte_Id);

        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            imgUrl img = new imgUrl
            {
                Url = reader.GetString(0),
                Producte_id = reader.GetGuid(1)
            };

            images.Add(img);
        }

        dbConn.Close();

        return images;
    }

    public static string? GetUrlByProductId(TaverDBConnection dbConn, Guid producte_Id)
    {
        dbConn.Open();

        string sql = @"SELECT url
                       FROM img_url
                       WHERE producte_id = @ProducteId";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@ProducteId", producte_Id);

        using SqlDataReader reader = cmd.ExecuteReader();

        string? result = null;
        if (reader.Read())
        {
            result = reader.GetString(0);
        }

        dbConn.Close();

        return result;
    }
}