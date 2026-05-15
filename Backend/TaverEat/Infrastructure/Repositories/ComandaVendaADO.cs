namespace Infrastructure.Repositories;

using Microsoft.Data.SqlClient;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using API.Services;

public class ComandaVendaADO : IComandaVendaRepository
{
    private readonly TaverDBConnection _dbConn;

    public ComandaVendaADO(TaverDBConnection dbConn)
    {
        _dbConn = dbConn;
    }

    public ComandaVenda? GetComandaActivaByClient(Guid clientId)
    {
        _dbConn.Open();

        string sql = @"SELECT id, client_id, entrega_dir, data, estat
                       FROM comanda_venda
                       WHERE client_id = @clientId AND estat = 'pendent'";

        using var cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@clientId", clientId);

        using var reader = cmd.ExecuteReader();

        ComandaVenda? comanda = null;

        if (reader.Read())
            comanda = ComandaVendaMapper.ToDomain(ReadEntity(reader));

        _dbConn.Close();
        return comanda;
    }

    public ComandaVenda CreateComanda(Guid clientId)
    {
        var entity = new ComandaVendaEntity
        {
            Id = Guid.NewGuid(),
            ClientId = clientId,
            Data = DateTime.UtcNow,
            Estat = "pendent"
        };

        _dbConn.Open();

        string sql = @"INSERT INTO comanda_venda (id, client_id, data, estat)
                       VALUES (@id, @clientId, @data, @estat)";

        using var cmd = new SqlCommand(sql, _dbConn.sqlConnection);

        cmd.Parameters.AddWithValue("@id", entity.Id);
        cmd.Parameters.AddWithValue("@clientId", entity.ClientId);
        cmd.Parameters.AddWithValue("@data", entity.Data);
        cmd.Parameters.AddWithValue("@estat", entity.Estat);

        cmd.ExecuteNonQuery();

        _dbConn.Close();

        return ComandaVendaMapper.ToDomain(entity);
    }

    public ComandaVendaLinea? GetLinea(Guid comandaId, Guid producteId)
    {
        _dbConn.Open();

        string sql = @"SELECT id, id_comanda_venda, producte_id, quantitat
                       FROM comanda_venda_linea
                       WHERE id_comanda_venda = @comandaId
                       AND producte_id = @producteId";

        using var cmd = new SqlCommand(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@comandaId", comandaId);
        cmd.Parameters.AddWithValue("@producteId", producteId);

        using var reader = cmd.ExecuteReader();

        ComandaVendaLinea? linea = null;

        if (reader.Read())
            linea = ComandaVendaLineaMapper.ToDomain(ReadLineaEntity(reader));

        _dbConn.Close();
        return linea;
    }

    public void AddLinea(ComandaVendaLinea linea)
    {
        _dbConn.Open();

        string sql = @"INSERT INTO comanda_venda_linea
                       (id, id_comanda_venda, producte_id, quantitat)
                       VALUES (@id, @comandaId, @producteId, @quantitat)";

        using var cmd = new SqlCommand(sql, _dbConn.sqlConnection);

        cmd.Parameters.AddWithValue("@id", Guid.NewGuid());
        cmd.Parameters.AddWithValue("@comandaId", linea.ComandaId);
        cmd.Parameters.AddWithValue("@producteId", linea.ProducteId);
        cmd.Parameters.AddWithValue("@quantitat", linea.Quantitat);

        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public void UpdateLinea(ComandaVendaLinea linea)
    {
        _dbConn.Open();

        string sql = @"UPDATE comanda_venda_linea
                       SET quantitat = @quantitat
                       WHERE id_comanda_venda = @comandaId
                       AND producte_id = @producteId";

        using var cmd = new SqlCommand(sql, _dbConn.sqlConnection);

        cmd.Parameters.AddWithValue("@quantitat", linea.Quantitat);
        cmd.Parameters.AddWithValue("@comandaId", linea.ComandaId);
        cmd.Parameters.AddWithValue("@producteId", linea.ProducteId);

        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public void DeleteLinea(Guid lineaId)
    {
        _dbConn.Open();

        string sql = "DELETE FROM comanda_venda_linea WHERE id = @id";

        using var cmd = new SqlCommand(sql, _dbConn.sqlConnection);

        cmd.Parameters.AddWithValue("@id", lineaId);

        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public void ConfirmarComanda(Guid comandaId)
    {
        _dbConn.Open();

        string sql = "UPDATE comanda_venda SET estat = 'confirmada' WHERE id = @id";

        using var cmd = new SqlCommand(sql, _dbConn.sqlConnection);

        cmd.Parameters.AddWithValue("@id", comandaId);

        cmd.ExecuteNonQuery();

        _dbConn.Close();
    }

    public List<(ComandaVendaLinea linea, Product producte)> GetLineasWithProducte(Guid comandaId)
    {
        _dbConn.Open();

        string sql = @"SELECT 
                        l.id,
                        l.id_comanda_venda,
                        l.producte_id,
                        l.quantitat,

                        p.id,
                        p.nom,
                        p.descripcio,
                        p.preu,
                        p.categoria_nom

                    FROM comanda_venda_linea l
                    JOIN producte p ON p.id = l.producte_id
                    WHERE l.id_comanda_venda = @comandaId";

        using var cmd = new SqlCommand(sql, _dbConn.sqlConnection);

        cmd.Parameters.AddWithValue("@comandaId", comandaId);

        using var reader = cmd.ExecuteReader();

        List<(ComandaVendaLinea linea, Product producte)> result = new();

        while (reader.Read())
        {
            var linea = ComandaVendaLineaMapper.ToDomain(
                ReadLineaEntity(reader)
            );

            var producte = new Product(
                reader.GetGuid(4),
                reader.GetString(5),
                reader.GetString(6),
                reader.GetDecimal(7),
                reader.GetString(8)
            );

            result.Add((linea, producte));
        }

        _dbConn.Close();

        return result;
    }

    private static ComandaVendaEntity ReadEntity(SqlDataReader r)
        => new ComandaVendaEntity
    {
        Id = r.GetGuid(0),
        ClientId = r.GetGuid(1),
        EntregaDir = r.IsDBNull(2) ? Guid.Empty : r.GetGuid(2),
        Data = r.GetDateTime(3),
        Estat = r.GetString(4)
    };

    private static ComandaVendaLineaEntity ReadLineaEntity(SqlDataReader r)
        => new ComandaVendaLineaEntity
    {
        Id = r.GetGuid(0),
        ComandaId = r.GetGuid(1),
        ProducteId = r.GetGuid(2),
        Quantitat = r.GetInt32(3)
    };
}