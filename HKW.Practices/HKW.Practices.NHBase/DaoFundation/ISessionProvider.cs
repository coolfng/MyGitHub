using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace HKW.Practices.NHBase.DaoFundation
{
    public interface ISessionProvider
    {
        ISession GetCurrentSession();
        void DisposeCurrentSession();
    }
}
