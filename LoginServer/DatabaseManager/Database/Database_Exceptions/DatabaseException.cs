using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Manager.Database.Database_Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message)
            : base(message)
        {
        }
    }
}

