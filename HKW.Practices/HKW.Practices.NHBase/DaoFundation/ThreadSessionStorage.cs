using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace HKW.Practices.NHBase.DaoFundation
{
    public class ThreadSessionStorage : ISessionStorage
    {
        [ThreadStatic]
        private static ISession _session;

        public ISession Get()
        {
            if (_session != null)
            {
                if (!_session.IsConnected)
                {
                    _session.Reconnect();
                }
            }
            return _session;
        }

        public void Set(ISession value)
        {
            if (value != null)
            {
                if (value.IsConnected)
                {
                    //value.Disconnect();
                }
            }
            else
            {
                _session.Dispose();
            }

            _session = value;
        }
    }
}
