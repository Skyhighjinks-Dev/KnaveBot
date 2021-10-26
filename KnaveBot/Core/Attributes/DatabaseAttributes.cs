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
      public string ColumnName;

      public DBAttribute() => ColumnName = null;

      public DBAttribute(string nName) => ColumnName = nName;
    }
  }
}
