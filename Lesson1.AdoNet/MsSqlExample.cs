using System.Data;
using Microsoft.Data.SqlClient;

namespace Lesson1.AdoNet;

public class MsSqlExample
{
    public void Example(string connectionString)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            //connection.Open();
            var sql = "SELECT * FROM ExampleTable";
            // command - IDbCommand
            using (var command = new SqlCommand(sql, connection))
            {
                // adapter - DbDataAdapter
                using (var adapter = new SqlDataAdapter(command))
                {
                    var resultTable = new DataTable();
                    adapter.Fill(resultTable);
                }
            }
        }
    }
}