using System;
using System.Collections.Generic;
using HKW.Practices.NHBase.Domain;
using NHibernate;

namespace HKW.Practices.NHBase.DaoFundation
{
    public interface IRepository : IDisposable
    {
        bool Save<T>(T entity) where T : Entity;
        bool BatchSave<T>(IList<T> list) where T : Entity;
        bool Update<T>(T entity) where T : Entity;
        bool SaveOrUpdate<T>(T entity) where T : Entity;
        bool Delete<T>(T entity) where T : Entity;
        T Get<T>(Guid id) where T : Entity;
        IList<T> FindAll<T>() where T : Entity;
        IList<T> Find<T>(string propertyName, object value) where T : Entity;
        ITransaction BeginTransaction();
    }
}
