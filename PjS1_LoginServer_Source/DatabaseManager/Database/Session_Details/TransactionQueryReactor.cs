using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using Database_Manager.Database;
    using Database_Manager.Database.Database_Exceptions;
    using Database_Manager.Database.Session_Details.Interfaces;
    using Database_Manager.Session_Details.Interfaces;
    using MySql.Data.MySqlClient;

namespace Database_Manager.Database.Session_Details
{
    
    internal class TransactionQueryReactor : QueryAdapter, IQueryAdapter, IRegularQueryAdapter, IDisposable
    {
        private bool finishedTransaction;
        private MySqlTransaction transaction;

        internal TransactionQueryReactor(MySqlClient client)
            : base(client)
        {
            this.initTransaction();
        }

        public void Dispose()
        {
            if (!this.finishedTransaction)
            {
                throw new TransactionException("The transaction needs to be completed by commit() or rollback() before you can dispose this item.");
            }
            base.command.Dispose();
            base.client.reportDone();
        }

        public void doCommit()
        {
            try
            {
                this.transaction.Commit();
                this.finishedTransaction = true;
            }
            catch (MySqlException exception)
            {
                throw new TransactionException(exception.Message);
            }
        }

        public void doRollBack()
        {
            try
            {
                this.transaction.Rollback();
                this.finishedTransaction = true;
            }
            catch (MySqlException exception)
            {
                throw new TransactionException(exception.Message);
            }
        }

        internal bool getAutoCommit()
        {
            return false;
        }

        private void initTransaction()
        {
            base.command = base.client.getNewCommand();
            this.transaction = base.client.getTransaction();
            base.command.Transaction = this.transaction;
            base.command.Connection = this.transaction.Connection;
            this.finishedTransaction = false;
        }
    }
}

