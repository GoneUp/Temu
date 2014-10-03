using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Manager.Session_Details.Interfaces;


namespace Database_Manager.Database.Session_Details.Interfaces
{
    public interface IQueryAdapter : IRegularQueryAdapter, IDisposable
    {
        void doCommit();
        void doRollBack();
        long insertQuery();
        void runQuery();
    }
}

