using KnaveBot.Core.Enum.DatabaseEnum;
using KnaveBot.Core.Struct.Discord;

namespace KnaveBot.Core.Struct.DatabaseStruct
{
  public struct DBLogging
  {
    public DBLogType LogSeverity { get; private set; }
    public CommandLoggingInfo Information { get; private set; }
    public string AdditionalInfo { get; set; }

    public DBLogging(DBLogType nType, CommandLoggingInfo nInfo, string nAdditionalInfo)
    {
      this.LogSeverity = nType;
      this.Information = nInfo;
      this.AdditionalInfo = nAdditionalInfo;
    }
  }
}
