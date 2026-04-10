using Microsoft.Data.SqlClient;

namespace API.Services
{
    public class TaverDBConnection
    {
        private readonly string _connectionString;
        public SqlConnection? sqlConnection;

        public TaverDBConnection(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public TaverDBConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection!;
        }

        public bool Open()
        {
            sqlConnection = new SqlConnection(_connectionString);

            try
            {
                sqlConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public void Close()
        {
            sqlConnection?.Close();
        }
    }
}