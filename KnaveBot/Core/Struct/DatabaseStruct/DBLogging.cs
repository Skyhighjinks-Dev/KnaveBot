using KnaveBot.Core.Enum.DatabaseEnum;
using KnaveBot.Core.Struct.Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
