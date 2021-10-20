using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KnaveBot.Database
{
  public class DatabaseManager
  {
    public SqlConnection SqlInstance { get; set; }
    public static DatabaseManager Instance { get; private set; }

    public DatabaseManager()
    {
      this.SqlInstance = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseSrv1"].ConnectionString);

      this.SqlInstance.Open();

      Instance = this;
    }

    public async Task<string> GetConfig(string nConfigValue) => await new Database().GetConfigOption(nConfigValue);
  }
}
