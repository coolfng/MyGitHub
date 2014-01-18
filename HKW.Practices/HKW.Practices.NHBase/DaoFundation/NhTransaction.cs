using System;
using NHibernate;

namespace HKW.Practices.NHBase.DaoFundation
{
    public class NhTransaction : ITransaction
    {
        ITransaction transaction;

        bool isOriginator = true;

        public NhTransaction(ISession session)
        {
            transaction = session.Transaction;

            if (transaction.IsActive)
                isOriginator = false; // The method that first opened the transaction should also close it
            else
                transaction.Begin();
        }

        #region ITransaction Members

        public void Commit()
        {
            if (isOriginator && !transaction.WasCommitted && !transaction.WasRolledBack)
                transaction.Commit();
        }

        public void Rollback()
        {
            if (!transaction.WasCommitted && !transaction.WasRolledBack)
                transaction.Rollback();
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (isOriginator)
            {
                Rollback();
                transaction.Dispose();
            }

        }

        #endregion

        public void Begin(System.Data.IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public void Begin()
        {
            throw new NotImplementedException();
        }

        public void Enlist(System.Data.IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public bool IsActive
        {
            get { throw new NotImplementedException(); }
        }

        public void RegisterSynchronization(NHibernate.Transaction.ISynchronization synchronization)
        {
            throw new NotImplementedException();
        }

        public bool WasCommitted
        {
            get { throw new NotImplementedException(); }
        }

        public bool WasRolledBack
        {
            get { throw new NotImplementedException(); }
        }
    }
}
