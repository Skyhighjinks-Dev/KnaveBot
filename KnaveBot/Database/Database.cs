using Discord;
using Discord.WebSocket;
using KnaveBot.Core.Aspects;
using KnaveBot.Core.Enum.Discord;
using KnaveBot.Database.Objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static KnaveBot.Core.Attributes.DatabaseAttributes;

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

    [DBConnAspect]
    public async Task<int> AddAdminAction(AdminAction nAction, IGuild nGuild, SocketGuildUser nUser, IUser nSender, string nReason)
    {
      string query = "INSERT INTO [dbo].[AdminAction] ([GuildID], [User], [UserID], [Sender], [SenderID], [Action], [Date], [InvalidDate], [Status], [LastStatusUpdate]) VALUES " +
                                                     "(@guild, @user, @userID, @sender, @senderID, @action, GETDATE(), null, @status, GETDATE());" +
                                                     "SELECT SCOPE_IDENTITY()";

      using (SqlCommand cmd = new SqlCommand(query, SqlInstance))
      {
        cmd.Parameters.AddWithValue("@guild", nGuild.Id.ToString());
        cmd.Parameters.AddWithValue("@user", nUser.Username);
        cmd.Parameters.AddWithValue("@userID", nUser.Id.ToString());
        cmd.Parameters.AddWithValue("@sender", nSender.Username);
        cmd.Parameters.AddWithValue("@senderID", nSender.Id.ToString());
        cmd.Parameters.AddWithValue("@action", Enum.GetName(typeof(AdminAction), nAction));
        cmd.Parameters.AddWithValue("@status", "pending");


        return Convert.ToInt32(await cmd.ExecuteScalarAsync().ConfigureAwait(false));
      }
    }

    [DBConnAspect]
    public async Task<bool> UpdateAdminActionToCompleteAsync(int nActionID, bool nComplete)
    {
      string query = "UPDATE [AdminAction] SET [Status] = @status, [LastStatusUpdate] = GETDATE() WHERE [ActionID] = @id";

      using (SqlCommand cmd = new SqlCommand(query, SqlInstance))
      {
        cmd.Parameters.AddWithValue("@status", nComplete ? "Completed" : "Pending");
        cmd.Parameters.AddWithValue("@id", nActionID);

        return await cmd.ExecuteNonQueryAsync() >= 1;
      }
    }

    public async Task<List<ActivityData>> GetActivity(SocketGuildUser nUser)
    {
      List<ActivityData> _rtn = new List<ActivityData>();

      string query = "SELECT * FROM [dbo].[AdminAction] WHERE [UserID] = @userId OR [SenderID] = @senderID";

      try
      {
        using (SqlCommand cmd = new SqlCommand(query, SqlInstance))
        {
          cmd.Parameters.AddWithValue("@userId", nUser.Id.ToString());
          cmd.Parameters.AddWithValue("@senderID", nUser.Id.ToString());

          using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            Dictionary<string, string> _list = new Dictionary<string, string>();

            while (reader.Read())
            {
              _rtn.Add(new ActivityData()
              {
                ActionID = GetData<int>(reader, GetColumnName(nameof(ActivityData.ActionID))),
                Action = GetData<AdminAction>(reader, GetColumnName(nameof(ActivityData.Action))),
                ActionDate = GetData<DateTime?>(reader, GetColumnName(nameof(ActivityData.ActionDate))),
                ExpiryDate = GetData<DateTime?>(reader, GetColumnName(nameof(ActivityData.ExpiryDate))),
                GuildID = GetData<ulong>(reader, GetColumnName(nameof(ActivityData.GuildID))),
                LastUpdateStatus = GetData<DateTime?>(reader, GetColumnName(nameof(ActivityData.LastUpdateStatus))),
                Sender = GetData<string>(reader, GetColumnName(nameof(ActivityData.Sender))),
                SenderID = GetData<ulong>(reader, GetColumnName(nameof(ActivityData.SenderID))),
                Status = GetData<string>(reader, GetColumnName(nameof(ActivityData.Status))),
                User = GetData<string>(reader, GetColumnName(nameof(ActivityData.User))),
                UserID = GetData<ulong>(reader, GetColumnName(nameof(ActivityData.UserID)))
              });
            }
          }
        }
      }
      catch (Exception e)
      {

      }

      return _rtn;
    }

    private T? GetData<T>(SqlDataReader nReader, string nValueName)
    {
      if (typeof(T).IsEnum && typeof(T).Name == nameof(AdminAction))
        return (T)Enum.Parse(typeof(AdminAction), nReader.GetString(nReader.GetOrdinal(nValueName)), true);

      switch (Type.GetTypeCode(typeof(T)))
      {
        case TypeCode.Int32:
          return (T)Convert.ChangeType(nReader.GetInt32(nReader.GetOrdinal(nValueName)), typeof(T));

        case TypeCode.String:
          return (T)Convert.ChangeType(nReader.GetString(nReader.GetOrdinal(nValueName)), typeof(T));

        case TypeCode.UInt64:
          return (T)Convert.ChangeType(nReader.GetInt64(nReader.GetOrdinal(nValueName)), typeof(T));

        case TypeCode.DateTime:
          if (nReader.IsDBNull(nReader.GetOrdinal(nValueName)))
            return (T)Convert.ChangeType(null, typeof(T));
          return (T)Convert.ChangeType(nReader.GetDateTime(nReader.GetOrdinal(nValueName)), typeof(T));
      }

      return default(T);
    }

    private string GetColumnName(string nFieldName)
    {
      return ((DBAttribute)(typeof(ActivityData).GetField(nFieldName).GetCustomAttributes(typeof(DBAttribute), false).First())).ColumnName;
    }
  }
}
