using NHibernate;

namespace HKW.Practices.NHBase.DaoFundation
{
    public interface ISessionStorage
    {
        ISession Get();
        void Set(ISession value);
    }

    public enum SessionStorageType
    {
        Http = 0,
        Thread = 1
    }
}
