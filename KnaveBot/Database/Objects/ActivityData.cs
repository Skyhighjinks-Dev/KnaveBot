using System;

using KnaveBot.Core.Enum.Discord;
using static KnaveBot.Core.Attributes.DatabaseAttributes;

namespace KnaveBot.Database.Objects
{
  /// <summary>
  /// Contains information about an Admin action
  /// </summary>
  public class ActivityData
  {
    [DBAttribute("ActionID")]
    public int ActionID;

    [DBAttribute("GuildID")]
    public ulong GuildID;

    [DBAttribute("User")]
    public string User;

    [DBAttribute("UserID")]
    public ulong UserID;

    [DBAttribute("Sender")]
    public string Sender;

    [DBAttribute("SenderID")]
    public ulong SenderID;

    [DBAttribute("Action")]
    public AdminAction Action;

    [DBAttribute("Date")]
    public DateTime? ActionDate;

    [DBAttribute("InvalidDate")]
    public DateTime? ExpiryDate;

    [DBAttribute("Status")]
    public string Status;

    [DBAttribute("LastStatusUpdate")]
    public DateTime? LastUpdateStatus;
  }
}
