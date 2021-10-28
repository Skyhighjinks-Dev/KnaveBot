using KnaveBot.Core.Enum.DatabaseEnum;
using KnaveBot.Core.Struct.Discord;

namespace KnaveBot.Core.Struct.DatabaseStruct
{
  public struct DBLogging
  {
    /// <summary>
    /// Information about the command issue
    /// </summary>
    public DBLogType LogSeverity { get; private set; }
    public CommandLoggingInfo Information { get; private set; }
    public string AdditionalInfo { get; set; }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="nType">Type of error</param>
    /// <param name="nInfo">Command information</param>
    /// <param name="nAdditionalInfo">Additional Infomation</param>
    public DBLogging(DBLogType nType, CommandLoggingInfo nInfo, string nAdditionalInfo)
    {
      this.LogSeverity = nType;
      this.Information = nInfo;
      this.AdditionalInfo = nAdditionalInfo;
    }
  }
}
