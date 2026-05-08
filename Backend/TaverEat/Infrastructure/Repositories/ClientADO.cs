using Microsoft.Data.SqlClient;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using API.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Infrastructure.Repositories;

public class ClientADO : IClientRepository
{
    private readonly TaverDBConnection _dbConn;

    public ClientADO(TaverDBConnection dbConn)
    {
        _dbConn = dbConn;
    }

    public List<Client> GetAll()
    {
        List<Client> clients = new();
        _dbConn.Open();

        string sql = "SELECT id, nom, cognom, email, direccio, contrasenya FROM client";
        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            clients.Add(ClientMapper.ToDomain(ReadEntity(reader)));

        _dbConn.Close();
        return clients;
    }


    public Client GetById(Guid id)
    {
        _dbConn.Open();
        string sql = "SELECT id, nom, cognom, email, direccio, contrasenya FROM client WHERE id = @id";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@id", id);
        using SqlDataReader reader = cmd.ExecuteReader();

        Client? client = null;
        if(reader.Read())
            client = ClientMapper.ToDomain(ReadEntity(reader));

        _dbConn.Close();
        if (client == null) throw new Exception("Client not found");
        return client;
    }

    public void Insert(Client client)
    {
        _dbConn.Open();
        string sql = @"INSERT INTO client (id, nom, cognom, email, direccio, contrasenya)
                        VALUES (@id, @nom, @cognom, @email, @direccio, @contrasenya)";
        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        var entity = ClientMapper.ToEntity(client);
        cmd.Parameters.AddWithValue("@id", entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id);
        cmd.Parameters.AddWithValue("@nom", entity.Nom);
        cmd.Parameters.AddWithValue("@cognom", entity.Cognom);
        cmd.Parameters.AddWithValue("@email", entity.Email);
        cmd.Parameters.AddWithValue("@direccio", entity.Direccio);
        cmd.Parameters.AddWithValue("@contrasenya", entity.Contrasenya);
        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public void Update(Client client)
    {
        _dbConn.Open();
        string sql = @"UPDATE client SET
                        nom = @nom, cognom = @cognom, email = @email,
                       direccio = @direccio, contrasenya = @contrasenya
                       WHERE id = @id";

         using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        var entity = ClientMapper.ToEntity(client);
        cmd.Parameters.AddWithValue("@id", entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id);
        cmd.Parameters.AddWithValue("@nom", entity.Nom);
        cmd.Parameters.AddWithValue("@cognom", entity.Cognom);
        cmd.Parameters.AddWithValue("@email", entity.Email);
        cmd.Parameters.AddWithValue("@direccio", entity.Direccio);
        cmd.Parameters.AddWithValue("@contrasenya", entity.Contrasenya);
        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public bool Delete(Guid id)
    {
        _dbConn.Open();
        string sql = "DELETE FROM client WHERE id = @id";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@id", id);
        int rows = cmd.ExecuteNonQuery();

        _dbConn.Close();
        return rows > 0;
    }

        private static ClientEntity ReadEntity(SqlDataReader r) => new ClientEntity
    {
        Id = r.GetGuid(0),
        Nom = r.GetString(1),
        Cognom = r.GetString(2),
        Email = r.GetString(3),
        Direccio = r.GetString(4),
        Contrasenya = r.GetString(5)
    };

}