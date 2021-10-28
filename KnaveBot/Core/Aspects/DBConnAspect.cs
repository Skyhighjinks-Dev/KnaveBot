using KnaveBot.Database;

using PostSharp.Aspects;
using PostSharp.Serialization;

using System;
using System.Threading.Tasks;

namespace KnaveBot.Core.Aspects
{
  [PSerializable]
  public class DBConnAspect : OnMethodBoundaryAspect
  {
    /// <summary>
    /// Pre method execution
    /// </summary>
    /// <param name="args">Method information</param>
    public override void OnEntry(MethodExecutionArgs args)
    {
      // Checks the SqlConnection to see if it's already open
      if (RetrieveManager(args).SqlInstance.State != System.Data.ConnectionState.Open)
      {
        // Attempts to open
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


    /// <summary>
    /// Post method exeuction
    /// </summary>
    /// <param name="args">Method information</param>
    public override void OnExit(MethodExecutionArgs args)
    {
      // Checks to see if the SqlConnection is already closed
      if (RetrieveManager(args).SqlInstance.State != System.Data.ConnectionState.Closed)
      {
        // Attempts to close the connection
        try
        {
          Task.Run(async () => await RetrieveManager(args).SqlInstance.CloseAsync());
        }
        catch (Exception)
        { 
        
        }
      }
    }


    /// <summary>
    /// Retrieves the DatabaseManager object
    /// </summary>
    /// <param name="nArgs">Method Information</param>
    /// <returns>DatabaseManager object</returns>
    private DatabaseManager RetrieveManager(MethodExecutionArgs nArgs)
    {
      return (DatabaseManager)nArgs.Instance;
    }
  }
}
