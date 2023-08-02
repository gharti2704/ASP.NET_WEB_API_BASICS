using Microsoft.Data.SqlClient;

namespace basic.Helpers;
public class Helpers
{
  public static async Task<bool> ExecuteSqlWithParameters(string sql, List<SqlParameter> parameters)
  {
    var commandWithParams = new SqlCommand(sql);

    foreach (SqlParameter parameter in parameters)
    {
      commandWithParams.Parameters.Add(parameter);
    }

    var dbConnection = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
    dbConnection.Open();

    commandWithParams.Connection = dbConnection;
    int rowsAffected = await commandWithParams.ExecuteNonQueryAsync();

    dbConnection.Close();

    return rowsAffected > 0;
  }

}