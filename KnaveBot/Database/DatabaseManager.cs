using Discord;
using Discord.WebSocket;
using KnaveBot.Core.Enum.Discord;
using System.Collections.Generic;
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

    public async Task<int> InsertAdminActionAsync(AdminAction nAction, IGuild nGuild, SocketGuildUser nUser, IUser nSender, string nReason = null) => await new Database().AddAdminAction(nAction, nGuild, nUser, nSender, nReason);

    public async Task<bool> UpdateAdminActionAsync(int nActionID, bool nComplete) => await new Database().UpdateAdminActionToCompleteAsync(nActionID, nComplete);

    public async Task<List<Objects.ActivityData>> GetActivityData(SocketGuildUser nUser) => await new Database().GetActivity(nUser);
  }
}
