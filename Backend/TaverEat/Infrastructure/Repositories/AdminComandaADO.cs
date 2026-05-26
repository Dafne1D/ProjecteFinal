using Microsoft.Data.SqlClient;
using Domain.Entities;
using Infrastructure.Interfaces;
using API.Services;

namespace Infrastructure.Repositories;

public class AdminComandaADO : IAdminComandaRepository
{
    private readonly TaverDBConnection _dbConn;

    public AdminComandaADO(TaverDBConnection dbConn)
    {
        _dbConn = dbConn;
    }

    // GET ALL (ADMIN VIEW)
    public List<ComandaVenda> GetAll()
    {
        var comandes = new List<ComandaVenda>();

        _dbConn.Open();

        string sql = @"
            SELECT id, client_id, entrega_dir, data, estat
            FROM comanda_venda
            ORDER BY data DESC";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string? entregaDir = reader.IsDBNull(reader.GetOrdinal("entrega_dir"))
                ? null
                : reader.GetString(reader.GetOrdinal("entrega_dir"));

            var comanda = new ComandaVenda(
                reader.GetGuid(reader.GetOrdinal("id")),
                reader.GetGuid(reader.GetOrdinal("client_id")),
                entregaDir,
                reader.GetDateTime(reader.GetOrdinal("data")),
                reader.GetString(reader.GetOrdinal("estat"))
            );

            comandes.Add(comanda);
        }

        _dbConn.Close();
        return comandes;
    }

    // GET BY ID
    public ComandaVenda? GetById(Guid id)
    {
        _dbConn.Open();

        string sql = @"
            SELECT id, client_id, entrega_dir, data, estat
            FROM comanda_venda
            WHERE id = @id";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (!reader.Read())
        {
            _dbConn.Close();
            return null;
        }

        string? entregaDir = reader.IsDBNull(reader.GetOrdinal("entrega_dir"))
            ? null
            : reader.GetString(reader.GetOrdinal("entrega_dir"));

        var comanda = new ComandaVenda(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetGuid(reader.GetOrdinal("client_id")),
            entregaDir,
            reader.GetDateTime(reader.GetOrdinal("data")),
            reader.GetString(reader.GetOrdinal("estat"))
        );

        _dbConn.Close();
        return comanda;
    }

    // GET LINEAS
    public List<ComandaVendaLinea> GetLineas(Guid comandaId)
    {
        var lineas = new List<ComandaVendaLinea>();

        _dbConn.Open();

        string sql = @"
            SELECT id, id_comanda_venda, producte_id, quantitat
            FROM comanda_venda_linea
            WHERE id_comanda_venda = @id";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@id", comandaId);

        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lineas.Add(new ComandaVendaLinea(
                reader.GetGuid(0),
                reader.GetGuid(1),
                reader.GetGuid(2),
                reader.GetInt32(3)
            ));
        }

        _dbConn.Close();
        return lineas;
    }

    // UPDATE ESTAT
    public void UpdateEstat(Guid comandaId, string estat)
    {
        _dbConn.Open();

        string sql = @"
            UPDATE comanda_venda
            SET estat = @estat
            WHERE id = @id";

        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);

        cmd.Parameters.AddWithValue("@id", comandaId);
        cmd.Parameters.AddWithValue("@estat", estat);

        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }
}