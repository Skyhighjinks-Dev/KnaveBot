using KnaveBot.Core.Aspects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnaveBot.Database
{
  public class Database : DatabaseManager
  {
    [DBConnAspect]
    public async Task<string> GetConfigOption(string nConfOpt)
    {
      try
      {
        string query = "SELECT TOP(1) [Value] FROM [dbo].[Config] WHERE [Name] = @nConfigOpt";

        using (SqlCommand cmd = new SqlCommand(query, SqlInstance))
        {
          cmd.Parameters.AddWithValue("@nConfigOpt", nConfOpt);

          using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            if (!reader.HasRows)
            {
              // Log Error
              return null;
            }


            while (await reader.ReadAsync())
            {
              return reader.GetString(reader.GetOrdinal("Value"));
            }
          }
        }
      }
      catch (Exception ex)
      {
        // Log Exception
      }

      return null;
    }
  }
}
