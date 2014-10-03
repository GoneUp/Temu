using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System.Data;
    using Database_Manager.Database;

namespace Database_Manager.Session_Details.Interfaces
{


    public interface IRegularQueryAdapter
    {
        void addParameter(string name, object query);
        bool findsResult();
        int getInteger();
        DataRow getRow();
        string getString();
        DataTable getTable();
        void runFastQuery(string query);
        void setQuery(string query);
        DatabaseType dbType { get; }
    }
}

