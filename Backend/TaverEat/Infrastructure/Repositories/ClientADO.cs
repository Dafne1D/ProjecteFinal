using Microsoft.Data.SqlClient;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using API.Services;

namespace Infrastructure.Repositories;

public class ClientADO : IClientRepository
{
    private readonly TaverDBConnection _dbConn;

    public ClientADO(TaverDBConnection dbConn)
    {
        _dbConn = dbConn;
    }

    // GET ALL
    public List<Client> GetAll()
    {
        List<Client> clients = new();
        _dbConn.Open();

        string sql = "SELECT id, nom, email, direccio, contrasenya, role FROM client";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            clients.Add(ClientMapper.ToDomain(ReadEntity(reader)));

        _dbConn.Close();
        return clients;
    }

    // BY ID
    public Client GetById(Guid id)
    {
        _dbConn.Open();

        string sql = "SELECT id, nom, email, direccio, contrasenya, role FROM client WHERE id = @id";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        Client? client = null;

        if (reader.Read())
            client = ClientMapper.ToDomain(ReadEntity(reader));

        _dbConn.Close();

        if (client == null)
            throw new Exception("Client not found");

        return client;
    }

    // GET BY EMAIL
    public Client GetByEmail(string email)
    {
        _dbConn.Open();

        string sql = "SELECT id, nom, email, direccio, contrasenya, role FROM client WHERE email = @email";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@email", email);

        using SqlDataReader reader = cmd.ExecuteReader();

        Client? client = null;

        if (reader.Read())
            client = ClientMapper.ToDomain(ReadEntity(reader));

        _dbConn.Close();

        if (client == null)
            throw new Exception("Client not found");

        return client;
    }

    // INSERT
    public void Insert(Client client)
    {
        _dbConn.Open();

        string sql = @"INSERT INTO client (id, nom, email, direccio, contrasenya, role)
                       VALUES (@id, @nom, @email, @direccio, @contrasenya, @role)";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);

        var entity = ClientMapper.ToEntity(client);

        cmd.Parameters.AddWithValue("@id", entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id);
        cmd.Parameters.AddWithValue("@nom", entity.Nom);
        cmd.Parameters.AddWithValue("@email", entity.Email);
        cmd.Parameters.AddWithValue("@direccio", entity.Direccio);
        cmd.Parameters.AddWithValue("@contrasenya", entity.Contrasenya);
        cmd.Parameters.AddWithValue("@role", entity.Role);

        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    // UPDATE
    public void Update(Client client)
    {
        _dbConn.Open();

        string sql = @"UPDATE client SET
                        nom = @nom,
                        email = @email,
                        direccio = @direccio
                       WHERE id = @id";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);

        cmd.Parameters.AddWithValue("@id", client.Id);
        cmd.Parameters.AddWithValue("@nom", client.Nom);
        cmd.Parameters.AddWithValue("@email", client.Email);
        cmd.Parameters.AddWithValue("@direccio", client.Direccio);

        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    // DELETE
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

    // MAPPER
    private static ClientEntity ReadEntity(SqlDataReader r) => new ClientEntity
    {
        Id = r.GetGuid(0),
        Nom = r.GetString(1),
        Email = r.GetString(2),
        Direccio = r.GetString(3),
        Contrasenya = r.GetString(4),
        Role = r.GetString(5)
    };
}