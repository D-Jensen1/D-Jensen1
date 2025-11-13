using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

string connectionString = "Server=tcp:dj111025.database.windows.net,1433;Initial Catalog=AdventureWorksDW;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\"";
string queryString = """
    SELECT * 
    FROM dbo.vw_FYMoM
    """;
SqlConnection connection = new SqlConnection(connectionString);
connection.Open();

using(SqlDataReader reader = new SqlCommand(queryString, connection).ExecuteReader())
{
    if (reader.HasRows)
    {
        while (reader.Read())
        {
            Console.WriteLine($"{reader["FiscalYear"]} | {reader["MonthNumberOfYear"]} | {reader["FormattedMonthlySales"]}");
        }
    }
    else
    {
        Console.WriteLine("No rows found.");
    }

    reader.Close();
}


connection.Close();