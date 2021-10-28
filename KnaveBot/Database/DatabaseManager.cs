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
    /// <summary>
    /// Sql Instance
    /// </summary>
    public SqlConnection SqlInstance { get; set; }

    // Static DatabaseManager
    public static DatabaseManager Instance { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public DatabaseManager()
    {
      this.SqlInstance = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseSrv1"].ConnectionString);

      this.SqlInstance.Open();

      Instance = this;
    }

    /// <summary>
    /// Gets config
    /// </summary>
    /// <param name="nConfigValue">Config value</param>
    /// <returns>string of value</returns>
    public async Task<string> GetConfig(string nConfigValue) => await new Database().GetConfigOption(nConfigValue);

    /// <summary>
    /// Inserts admin action
    /// </summary>
    /// <param name="nAction">Action type</param>
    /// <param name="nGuild">Guild Info</param>
    /// <param name="nUser">Target user</param>
    /// <param name="nSender">Sender</param>
    /// <param name="nReason">Reason</param>
    /// <returns>Action ID</returns>
    public async Task<int> InsertAdminActionAsync(AdminAction nAction, IGuild nGuild, SocketGuildUser nUser, IUser nSender, string nReason = null) => await new Database().AddAdminAction(nAction, nGuild, nUser, nSender, nReason);

    /// <summary>
    /// Updates admin action
    /// </summary>
    /// <param name="nActionID">Action ID</param>
    /// <param name="nComplete">If the action is completed</param>
    /// <returns>If update was successful</returns>
    public async Task<bool> UpdateAdminActionAsync(int nActionID, bool nComplete) => await new Database().UpdateAdminActionToCompleteAsync(nActionID, nComplete);

    /// <summary>
    /// Retrieves admin action data
    /// </summary>
    /// <param name="nUser">Target user</param>
    /// <returns>List of activity data</returns>
    public async Task<List<Objects.ActivityData>> GetActivityData(SocketGuildUser nUser) => await new Database().GetActivity(nUser);
  }
}
