// Infrastructure/Repositories/CategoriaRepository.cs
using Microsoft.Data.SqlClient;
using Domain.Entities;
using Infrastructure.Interfaces;
using API.Services;

namespace Infrastructure.Repositories;

public class CategoryADO : ICategoryRepository
{
    private readonly TaverDBConnection _dbConn;

    public CategoryADO(TaverDBConnection dbConn)
    {
        _dbConn = dbConn;
    }

    public List<Categoria> GetAll()
    {
        List<Categoria> categories = new();
        _dbConn.Open();

        string sql = "SELECT Nom FROM Categoria";
        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            categories.Add(new Categoria { Nom = reader.GetString(0) });

        _dbConn.Close();
        return categories;
    }

    public Categoria? GetByNom(string nom)
    {
        _dbConn.Open();
        string sql = "SELECT Nom FROM Categoria WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", nom);
        using SqlDataReader reader = cmd.ExecuteReader();

        Categoria? categoria = null;
        if (reader.Read())
            categoria = new Categoria { Nom = reader.GetString(0) };

        _dbConn.Close();
        return categoria;
    }

    public void Insert(Categoria categoria)
    {
        _dbConn.Open();
        string sql = "INSERT INTO Categoria (Nom) VALUES (@Nom)";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", categoria.Nom);
        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public void Update(Categoria categoria)
    {
        // Implementa quan tinguis camps a actualitzar
        throw new NotImplementedException();
    }

    public bool Delete(string nom)
    {
        _dbConn.Open();
        string sql = "DELETE FROM Categoria WHERE Nom = @Nom";

        using SqlCommand cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Nom", nom);
        int rows = cmd.ExecuteNonQuery();

        _dbConn.Close();
        return rows > 0;
    }
}