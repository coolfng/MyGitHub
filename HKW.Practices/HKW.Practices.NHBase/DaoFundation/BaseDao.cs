using System;
using System.Collections.Generic;
using System.Linq;
using HKW.Practices.NHBase.Domain;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace HKW.Practices.NHBase.DaoFundation
{
    /// <summary>
    /// 基础DAO
    /// </summary>
    public class BaseDao : IRepository
    {
        protected readonly ISession _session;

        public BaseDao()
        {
            _session = SessionProvider.GetCurrentSession();
        }

        public string OptTerm { get; set; }
        public string OptUser { get; set; }

        private void SetEntiyInfo<T>(T e) where T : Entity
        {
            e.OptDate = DateTime.Now;
            e.OptTerm = OptTerm;
            e.OptUser = OptUser;
        }

        public ISession Session
        {
            get { return _session; }
        }

        #region 基础增删改查

        /// <summary>
        /// 根据ID得到指定数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(Guid id) where T : Entity
        {
            var entity = _session.Get<T>(id);
            if (entity==null)
            {
                return null;
            }
            else
            {
                return entity;
            }
        }

        /// <summary>
        /// 根据传入的Domain类型得到其所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetObjects<T>() where T : Entity
        {
            try
            {
                return from obj in _session.Query<T>()
                       select obj;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("ERROR - BaseDao.GetObjects - {0}", ex.Message));
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<T> GetObjects<T>(string propertyName, object value) where T : Entity
        {
            try
            {
                return from obj in _session.Query<T>()
                       select obj;
            }
            catch (Exception ex)
            {
                Logger.Write(string.Format("ERROR - BaseDao.GetObjects(string propertyName, object value) - {0}", ex.Message));
                throw new Exception(ex.Message);
            }
        }

        public IList<T> FindAll<T>() where T : Entity
        {
            ICriteria criteria = _session.CreateCriteria(typeof(T));
            return criteria.List<T>();
        }


        public IList<T> Find<T>(string propertyName, object value) where T : Entity
        {
            if (value is string)
            {
                return _session.CreateCriteria<T>()
                    .Add(Restrictions.Like(propertyName, value))
                    .List<T>();
            }
            return _session.CreateCriteria<T>()
                .Add(Restrictions.Eq(propertyName, value))
                .List<T>();
        }

        public IList<T> Find<T>(params ICriterion[] criteria) where T : Entity
        {
            ICriteria crit = RepositoryHelper<T>.CreateCriteriaFromArray(_session, criteria);
            return crit.List<T>();
        }

        public IList<T> Find<T>(ICriteria criteria) where T : Entity
        {
            return criteria.List<T>();
        }

        public IList<T> Find<T>(Order[] orders, params ICriterion[] criteria) where T : Entity
        {
            ICriteria crit = RepositoryHelper<T>.CreateCriteriaFromArray(_session, criteria);
            foreach (Order order in orders)
            {
                crit.AddOrder(order);
            }
            return crit.List<T>();
        }

        public IList<T> Find<T>(int firstResult, int numberOfResults, Order[] selectionOrder, params ICriterion[] criteria) where T : Entity
        {
            ICriteria crit = RepositoryHelper<T>.CreateCriteriaFromArray(_session, criteria);
            crit.SetFirstResult(firstResult)
                .SetMaxResults(numberOfResults);
            foreach (Order order in selectionOrder)
            {
                crit.AddOrder(order);
            }
            return crit.List<T>();
        }

        public ITransaction BeginTransaction()
        {
            return new NhTransaction(_session);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Save<T>(T o) where T : Entity
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    SetEntiyInfo(o);
                    _session.Save(o);
                    _session.Flush();
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("ERROR - BaseDao.Save - {0}:{1}", DateTime.Now, ex.Message));
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 批量保存
        /// 
        /// 通过Session来进行批量操作会受到以下约束：
        ///（1）需要在Hibernate的配置文件中设置JDBC单次批量处理的数目，合理的取值通常为10到50之间，例如：
        ///hibernate.jdbc.batch_size=20
        ///进行批量操作时，应该保证每次向数据库发送的批量SQL语句数目与这个batch_size属性一致。
        ///（2）如果对象采用"identity"标识符生成器，则Hibernate无法在JDBC层进行批量插入操作。
        ///（3）进行批量操作时，建议关闭Hibernate的第二级缓存。
        ///hibernate.cache.use_second_level_cache=false 
        /// </summary>
        /// <returns></returns>
        public bool BatchSave<T>(IList<T> list) where T : Entity
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        SetEntiyInfo(list[i]);
                        _session.Save(list[i]);
                        if (i % 20 == 0) //单次批量操作的数目为20
                        {
                            _session.Flush(); //清理缓存，执行批量插入20条记录的SQL insert语句
                            _session.Clear(); //清空缓存中的Customer对象
                        }
                        //tx.Commit();
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("ERROR - BaseDao.BatchSave - {0}", ex.Message));
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 保存或者修改
        /// </summary>
        /// <param name="o"> </param>
        public bool SaveOrUpdate<T>(T o) where T : Entity
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    SetEntiyInfo(o);
                    _session.SaveOrUpdate(o);
                    _session.Flush();
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("ERROR - BaseDao.SaveOrUpdate - {0}:{1}", DateTime.Now, ex.Message));
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="o"></param>
        public bool Update<T>(T o) where T : Entity
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    SetEntiyInfo(o);
                    _session.Update(o);
                    _session.Flush();
                    _session.Clear(); //清除
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("ERROR - BaseDao.Edit - {0}:{1}", DateTime.Now, ex.Message));
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="o"></param>
        public bool Merge<T>(T o) where T : Entity
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    SetEntiyInfo(o);
                    _session.Merge(o);
                    _session.Flush();
                    _session.Clear(); //清除
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("ERROR - BaseDao.Edit - {0}:{1}", DateTime.Now, ex.Message));
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete<T>(T o) where T : Entity
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(o);
                    _session.Flush();
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("ERROR - BaseDao.Delete - {0}",  ex.Message));
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool ClearDelete<T>(T o) where T : Entity
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Clear();
                    _session.Delete(o);
                    _session.Flush();
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(string.Format("ERROR - BaseDao.ClearDelete - {0}",  ex.Message));
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Flush()
        {
            _session.Flush();
        }

        #endregion


        #region Dispose

        private bool _disposed; // 保证多次调用Dispose方式不会抛出异常

        /// <summary>
        /// 释放ISession
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //    GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // 在这里释放托管资源
            }

            // 在这里释放非托管资源
            //SessionProvider.CloseCurrentSession();
            _disposed = true;
        }

        /*
        ~BaseDao()
        {
            Dispose(false);
        }
        */

        #endregion
    }

    internal class RepositoryHelper<T>
    {
        public static ICriteria CreateCriteriaFromArray(ISession session, ICriterion[] criteria)
        {
            ICriteria crit = session.CreateCriteria(typeof(T));
            foreach (ICriterion criterion in criteria)
            {
                if (criterion == null)
                    continue;
                crit.Add(criterion);
            }

            return crit;
        }
    }
}
