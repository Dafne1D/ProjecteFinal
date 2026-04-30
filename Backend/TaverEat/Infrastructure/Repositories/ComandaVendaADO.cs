using Microsoft.Data.SqlClient;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using API.Services;
using System.Data.Common;

namespace Infrastructure.Repositories;

public class ComandaVendaADO : IComandaVendaRepository
{
    private readonly TaverDBConnection _dbConn;

    public ComandaVendaADO(TaverDBConnection dbConn)
    {
        _dbConn = dbConn;
    }

    public ComandaVenda? GetComandaActivaByClient(Guid clientId)
    {
        string sql = @"SELECT id, entrega_dir, data, estat, client_id FROM comanda_venda 
                    WHERE clientId = @client_id AND estat = 'pendent'";
        using SqlCommand cmd = new(sql, _dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@clientId", clientId);
        using SqlDataReader reader = cmd.ExecuteReader();

        ComandaVenda? comanda = null;
        if(reader.Read())
        {
            ComandaVenda = 
        }
    }


}