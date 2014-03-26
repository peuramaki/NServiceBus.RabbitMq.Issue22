using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using IsolationLevel = System.Data.IsolationLevel;

namespace DAL
{
    public class SqlServerClient 
    {

        public ISqlConnectionManager ConnectionManager { get; set; }

        public void CraeteSomething(Guid guid)
        {
            var sql = "INSERT INTO Something (Guid, Count) VALUES (@guid, 0)";
            using (var cmd = new SqlCommand(sql, this.ConnectionManager.Connection))
            {
                cmd.Transaction = this.ConnectionManager.Tran;
                cmd.Parameters.AddWithValue("guid", guid);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSomethingCount(Guid guid, int count)
        {
            var sql = "UPDATE Something SET Count = @count WHERE Guid = @guid";
            using (var cmd = new SqlCommand(sql, this.ConnectionManager.Connection))
            {
                cmd.Transaction = this.ConnectionManager.Tran;
                cmd.Parameters.AddWithValue("guid", guid);
                cmd.Parameters.AddWithValue("count", count);
                cmd.ExecuteNonQuery();
            }
        }

        public Tuple<Guid, int> GetSomething(Guid guid)
        {
            Tuple<Guid, int> res;
            var sql = "SELECT Guid, Count FROM Something WHERE Guid = @guid";

            using (var cmd = new SqlCommand(sql, this.ConnectionManager.Connection))
            {
                cmd.Transaction = this.ConnectionManager.Tran;
                cmd.Parameters.AddWithValue("guid", guid);
                using (var reader = cmd.ExecuteReader())
                {
                    res = reader.Read() ? new Tuple<Guid, int>(reader.GetGuid(0), reader.GetInt32(1)) : null;
                }

            }
            return res;
        }

 

    }
}
