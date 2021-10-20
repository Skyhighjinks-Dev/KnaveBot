using Discord.Commands;
using Discord.WebSocket;

using System;

namespace KnaveBot.Core.Struct.Discord
{
  public struct CommandLoggingInfo
  {
    public SocketUser Sender { get; private set; }
    public CommandContext Context { get; private set; }
    public string Arguments { get; private set; }
    public DateTime Raised { get; private set; }

    public CommandLoggingInfo(CommandContext nContext)
    {
      this.Context = nContext;
      this.Sender = (SocketUser)this.Context.Message.Author;
      this.Arguments = this.Context.Message.Content;
      this.Raised = DateTime.Now;
    }
  }
}
