using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlServerClient : IDisposable
    {
        private string _connectionStringName = "ServiceB/SqlConnection";
        private SqlConnection _connection = null;

        public SqlServerClient()
        {
            var connSettings = ConfigurationManager.ConnectionStrings[_connectionStringName];
            if (connSettings == null)
                throw new ConfigurationErrorsException(string.Format("Cannot find connection string '{0}'.", _connectionStringName));

            if (string.IsNullOrEmpty(connSettings.ConnectionString))
                throw new ConfigurationErrorsException(string.Format("Cannot find value for connection string '{0}'.", _connectionStringName));

            _connection = new SqlConnection(connSettings.ConnectionString);
            _connection.Open();
        }

        public void CraeteSomething(Guid guid)
        {
            var sql = "INSERT INTO Something (Guid, Count) VALUES (@guid, 0)";
            using (var cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("guid", guid);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSomethingCount(Guid guid, int count)
        {
            var sql = "UPDATE Something SET Count = @count WHERE Guid = @guid";
            using (var cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("guid", guid);
                cmd.Parameters.AddWithValue("count", count);
                cmd.ExecuteNonQuery();
            }
        }

        public Tuple<Guid, int> GetSomething(Guid guid)
        {
            var sql = "SELECT Guid, Count FROM Something WHERE Guid = @guid";

            using (var cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("guid", guid);
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.Read() ? new Tuple<Guid, int>(reader.GetGuid(0), reader.GetInt32(1)) : null;
                }
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
