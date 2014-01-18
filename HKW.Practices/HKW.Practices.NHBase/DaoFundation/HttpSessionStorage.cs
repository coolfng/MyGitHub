using System.Web;
using NHibernate;

namespace HKW.Practices.NHBase.DaoFundation
{
    public class HttpSessionStorage : ISessionStorage
    {
        private readonly string _currentSessionKey;

        public HttpSessionStorage()
        {
            _currentSessionKey = Config.SessionSourceType;
        }

        public ISession Get()
        {
            return (ISession)HttpContext.Current.Items[_currentSessionKey];
        }

        public void Set(ISession value)
        {
            if (value != null)
            {
                HttpContext.Current.Items.Add(_currentSessionKey, value);
            }
            else
            {
                HttpContext.Current.Items.Remove(_currentSessionKey);
            }
        }
    }
}
