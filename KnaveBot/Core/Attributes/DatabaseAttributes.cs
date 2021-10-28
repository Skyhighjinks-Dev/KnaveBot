using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnaveBot.Core.Attributes
{
  public class DatabaseAttributes
  {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class DBAttribute : Attribute
    { 
      /// <summary>
      /// Column name
      /// </summary>
      public string ColumnName;

      /// <summary>
      /// Empty constructor
      /// </summary>
      public DBAttribute() => ColumnName = null;

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="nName">Database Column Name</param>
      public DBAttribute(string nName) => ColumnName = nName;
    }
  }
}
