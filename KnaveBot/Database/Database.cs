using Discord;
using Discord.WebSocket;
using KnaveBot.Core.Aspects;
using KnaveBot.Core.Enum.Discord;
using KnaveBot.Database.Objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
              _rtn.Add(ExtractData<ActivityData>(reader));
            }
          }
        }
      }
      catch (Exception e)
      {

      }

      return _rtn;
    }

    public static T? GetData<T>(SqlDataReader nReader, string nValueName)
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

    public T ExtractData<T>(SqlDataReader nReader) where T : new()
    {
      T rtn = new T();

      foreach (FieldInfo f in typeof(ActivityData).GetFields())
      {
        Type _ = f.FieldType;

        MethodInfo method = typeof(Database).GetMethod(nameof(Database.GetData));
        MethodInfo generic = method.MakeGenericMethod(_);

        f.SetValue(rtn, generic.Invoke(null, new object[] { nReader, GetAttribute<DBAttribute, T>(f.Name).ColumnName }));
      }

      return rtn;
    }

    public T GetAttribute<T, U>(string nFieldName) where T : new()
    {
      return (T)(typeof(U).GetField(nFieldName).GetCustomAttributes(typeof(T), false).First());
    }
  }
}
