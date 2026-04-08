using System.Data;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly string _connectionString;

    public CartRepository(IConfiguration configuration)
    {
        // Change "DefaultConnection" if your connection string is named differently in appsettings.json
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<Cart?> GetCartByClientAsync(Guid clientId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        // 1. Get Cart Id
        Guid cartId = Guid.Empty;
        using var cartCmd = new SqlCommand("SELECT Id FROM Cart WHERE ClientId = @ClientId", connection);
        cartCmd.Parameters.AddWithValue("@ClientId", clientId);
        var res = await cartCmd.ExecuteScalarAsync();
        
        if (res == null) return null; // No cart exists
        cartId = (Guid)res;

        // 2. Get Cart Lines and their corresponding Products
        var lines = new List<CartLineaEntity>();
        var products = new List<ProductEntity>();

        using var linesCmd = new SqlCommand(@"
            SELECT cl.Id, cl.CartId, cl.ProductId, cl.Quantitat, 
                   p.nom, p.descripcio, p.preu
            FROM CartLinea cl
            INNER JOIN producte p ON cl.ProductId = p.id
            WHERE cl.CartId = @CartId", connection);
            
        linesCmd.Parameters.AddWithValue("@CartId", cartId);

        using var reader = await linesCmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var productId = reader.GetGuid(reader.GetOrdinal("ProductId"));

            lines.Add(new CartLineaEntity
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                CartId = reader.GetGuid(reader.GetOrdinal("CartId")),
                ProductId = productId,
                Quantitat = reader.GetInt32(reader.GetOrdinal("Quantitat"))
            });

            products.Add(new ProductEntity
            {
                Id = productId,
                Nom = reader.GetString(reader.GetOrdinal("nom")),
                Descripcio = reader.GetString(reader.GetOrdinal("descripcio")),
                Preu = reader.GetDecimal(reader.GetOrdinal("preu"))
            });
        }
        
        // Use the Mapper to assemble Domain Entity
        return CartMapper.ToDomain(lines, products);
    }

    public async Task<bool> AddProductToCartAsync(Guid clientId, Guid productId, int quantitat)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();

        try 
        {
            // 1. Ensure Cart exists, or create one
            using var cartCmd = new SqlCommand("SELECT Id FROM Cart WHERE ClientId = @ClientId", connection, transaction);
            cartCmd.Parameters.AddWithValue("@ClientId", clientId);
            var cartIdObj = await cartCmd.ExecuteScalarAsync();
            
            Guid cartId;
            if (cartIdObj == null) 
            {
                cartId = Guid.NewGuid();
                using var createCartCmd = new SqlCommand("INSERT INTO Cart (Id, ClientId) VALUES (@Id, @ClientId)", connection, transaction);
                createCartCmd.Parameters.AddWithValue("@Id", cartId);
                createCartCmd.Parameters.AddWithValue("@ClientId", clientId);
                await createCartCmd.ExecuteNonQueryAsync();
            }
            else 
            {
                cartId = (Guid)cartIdObj;
            }

            // 2. Check if product already in cart
            using var checkLineCmd = new SqlCommand("SELECT Id, Quantitat FROM CartLinea WHERE CartId = @CartId AND ProductId = @ProductId", connection, transaction);
            checkLineCmd.Parameters.AddWithValue("@CartId", cartId);
            checkLineCmd.Parameters.AddWithValue("@ProductId", productId);
            
            using (var reader = await checkLineCmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    // Update quantity
                    var lineId = reader.GetGuid(reader.GetOrdinal("Id"));
                    var currentQuantitat = reader.GetInt32(reader.GetOrdinal("Quantitat"));
                    reader.Close();

                    using var updateCmd = new SqlCommand("UPDATE CartLinea SET Quantitat = @Quantitat WHERE Id = @Id", connection, transaction);
                    updateCmd.Parameters.AddWithValue("@Quantitat", currentQuantitat + quantitat);
                    updateCmd.Parameters.AddWithValue("@Id", lineId);
                    await updateCmd.ExecuteNonQueryAsync();
                }
                else 
                {
                    reader.Close();
                    // Insert new line
                    using var insertCmd = new SqlCommand("INSERT INTO CartLinea (Id, CartId, ProductId, Quantitat) VALUES (@Id, @CartId, @ProductId, @Quantitat)", connection, transaction);
                    insertCmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    insertCmd.Parameters.AddWithValue("@CartId", cartId);
                    insertCmd.Parameters.AddWithValue("@ProductId", productId);
                    insertCmd.Parameters.AddWithValue("@Quantitat", quantitat);
                    await insertCmd.ExecuteNonQueryAsync();
                }
            }

            transaction.Commit();
            return true;
        }
        catch 
        {
            transaction.Rollback();
            return false;
        }
    }

    public async Task<bool> RemoveProductFromCartAsync(Guid clientId, Guid productId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var cmd = new SqlCommand(@"
            DELETE cl FROM CartLinea cl
            INNER JOIN Cart c ON cl.CartId = c.Id
            WHERE c.ClientId = @ClientId AND cl.ProductId = @ProductId", connection);
        
        cmd.Parameters.AddWithValue("@ClientId", clientId);
        cmd.Parameters.AddWithValue("@ProductId", productId);
        
        var affected = await cmd.ExecuteNonQueryAsync();
        return affected > 0;
    }

    public async Task<bool> ClearCartAsync(Guid clientId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        
        using var cmd = new SqlCommand(@"
            DELETE cl FROM CartLinea cl
            INNER JOIN Cart c ON cl.CartId = c.Id
            WHERE c.ClientId = @ClientId", connection);
            
        cmd.Parameters.AddWithValue("@ClientId", clientId);
        await cmd.ExecuteNonQueryAsync();
        
        return true;
    }

    public async Task<bool> ConvertCartToOrderAsync(Guid clientId)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();

        try
        {
            // 1. Get Cart
            using var cartCmd = new SqlCommand("SELECT Id FROM Cart WHERE ClientId = @ClientId", connection, transaction);
            cartCmd.Parameters.AddWithValue("@ClientId", clientId);
            var cartIdObj = await cartCmd.ExecuteScalarAsync();
            if (cartIdObj == null) return false;
            
            Guid cartId = (Guid)cartIdObj;

            // 2. Get Cart Lines
            using var linesCmd = new SqlCommand("SELECT ProductId, Quantitat FROM CartLinea WHERE CartId = @CartId", connection, transaction);
            linesCmd.Parameters.AddWithValue("@CartId", cartId);
            
            var lines = new List<(Guid ProductId, int Quantitat)>();
            using (var reader = await linesCmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    lines.Add((reader.GetGuid(reader.GetOrdinal("ProductId")), reader.GetInt32(reader.GetOrdinal("Quantitat"))));
                }
            }

            if (lines.Count == 0) return false;

            // 3. Create ComandaVenda
            Guid comandaId = Guid.NewGuid();
            using var insertComandaCmd = new SqlCommand("INSERT INTO comanda_venda (id, client_id, data, estat) VALUES (@Id, @ClientId, @Data, @Estat)", connection, transaction);
            insertComandaCmd.Parameters.AddWithValue("@Id", comandaId);
            insertComandaCmd.Parameters.AddWithValue("@ClientId", clientId);
            insertComandaCmd.Parameters.AddWithValue("@Data", DateTime.UtcNow);
            insertComandaCmd.Parameters.AddWithValue("@Estat", "Pendent"); 
            await insertComandaCmd.ExecuteNonQueryAsync();

            // 4. Create ComandaVendaLinea
            foreach (var line in lines)
            {
                using var insertLineaCmd = new SqlCommand("INSERT INTO comanda_venda_linea (id, id_comanda_venda, producte_id, quantitat) VALUES (@Id, @ComandaId, @ProductId, @Quantitat)", connection, transaction);
                insertLineaCmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                insertLineaCmd.Parameters.AddWithValue("@ComandaId", comandaId);
                insertLineaCmd.Parameters.AddWithValue("@ProductId", line.ProductId);
                insertLineaCmd.Parameters.AddWithValue("@Quantitat", line.Quantitat);
                await insertLineaCmd.ExecuteNonQueryAsync();
            }

            // 5. Clear Cart (delete lines)
            using var clearCartCmd = new SqlCommand("DELETE FROM CartLinea WHERE CartId = @CartId", connection, transaction);
            clearCartCmd.Parameters.AddWithValue("@CartId", cartId);
            await clearCartCmd.ExecuteNonQueryAsync();

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }
}
