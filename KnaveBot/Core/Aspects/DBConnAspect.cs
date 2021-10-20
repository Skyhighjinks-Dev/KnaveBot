using KnaveBot.Database;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnaveBot.Core.Aspects
{
  [PSerializable]
  public class DBConnAspect : OnMethodBoundaryAspect
  {
    public override void OnEntry(MethodExecutionArgs args)
    {
      if (RetrieveManager(args).SqlInstance.State != System.Data.ConnectionState.Open)
      {
        try
        {
          RetrieveManager(args).SqlInstance.Open();
        }
        catch (Exception)
        {
          // Log data if possible - store in file if not
        }
      }
    }


    public override void OnExit(MethodExecutionArgs args)
    {
      if (RetrieveManager(args).SqlInstance.State != System.Data.ConnectionState.Closed)
      {
        try
        {
          Task.Run(async () => await RetrieveManager(args).SqlInstance.CloseAsync());
        }
        catch (Exception)
        { 
        
        }
      }
    }


    private DatabaseManager RetrieveManager(MethodExecutionArgs nArgs)
    {
      return (DatabaseManager)nArgs.Instance;
    }
  }
}
