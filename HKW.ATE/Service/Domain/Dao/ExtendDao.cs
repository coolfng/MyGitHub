using System;
using System.Collections.Generic;
using System.Linq;
using HKW.ATE.Domain.Entities.STS;
using HKW.Practices.NHBase.DaoFundation;
using HKW.Practices.NHBase.Domain;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using NHibernate.Linq;

namespace HKW.ATE.Domain.Dao
{
    /// <summary>
    /// 扩展Dao.用于扩充BaseDao的方法
    /// 作者: 刘鑫
    /// 创建日期: 2013年6月6日
    /// 修改日期: 2013年7月18日
    /// 
    ///-------------------------
    /// 修改：刘世帅
    /// 修改日期: 2013年8月3日
    /// 修改：UpdateState
    /// ------------------------
    /// 修改: 刘世帅
    /// 修改日期: 2013年8月19日
    /// 修改内容: 去除DelFlag的查询限定
    /// </summary>
    public class ExtendDao : BaseDao
    {
        private const string ServerName = "ExtendDao";
        private Dictionary<Guid, object> _dic = new Dictionary<Guid, object>();
      
        public ExtendDao()
        {
            _session.Clear();
        }
        private void SetEntiyInfo<T>(T e) where T : Entity
        {
            e.OptDate = DateTime.Now;
            e.OptTerm = OptTerm;
            e.OptUser = OptUser;
        }

        public void TxCommit()
        {
            try
            {
                //for (int i = 0; i < tx.Count; i++)
                //{
                using (var transaction = _session.BeginTransaction())
                {
                    _session.Flush();
                    transaction.Commit();
                    _session.Clear();
                    foreach (var o in _dic.Values)
                    {
                        Session.Refresh(o);
                    }
                }


                //}

            }
            catch (Exception ex)
            {
                var message = Log(ex, ServerName);
                TxRollback();
                throw new Exception(message);
            }
        }

        public void TxRollback()
        {
            //for (int i = 0; i < tx.Count; i++)
            //{
            var transaction = _session.BeginTransaction();
            transaction.Rollback();
            //}
        }

        public void UnCommitSave<T>(T obj) where T : Entity
        {
            //using (ITransaction tx = _session.BeginTransaction())
            //{
            SetEntiyInfo(obj);
            _session.Save(obj);
            if (!_dic.ContainsValue(obj))
            {
                _dic.Add(Guid.NewGuid(), obj);
            }
            //_session.Flush();
            //    return tx;
            //}
        }

        public void UnCommitMerge<T>(T obj) where T : Entity
        {
            //using (ITransaction tx = _session.BeginTransaction())
            //{
            SetEntiyInfo(obj);
            _session.Merge(obj);
            if (!_dic.ContainsValue(obj))
            {
                _dic.Add(Guid.NewGuid(), obj);
            }
            //Session.Refresh(obj);
            //_session.Flush();
            //    return tx;
            //}
        }

        public void UnCommitDelete<T>(T obj) where T : Entity
        {
            //using (ITransaction tx = _session.BeginTransaction())
            //{
            //SetEntiyInfo(obj);
            _session.Delete(obj);
            if (!_dic.ContainsValue(obj))
            {
                _dic.Add(Guid.NewGuid(), obj);
            }
            //Session.Refresh(obj);
            //_session.Flush();
            //    return tx;
            //}
        }

        public void UnCommitSaveOrUpdate<T>(T obj) where T : Entity
        {
            //using (ITransaction tx = _session.BeginTransaction())
            //{
            SetEntiyInfo(obj);
            _session.SaveOrUpdate(obj);
            if (!_dic.ContainsValue(obj))
            {
                _dic.Add(Guid.NewGuid(), obj);
            }
            //_session.Flush();
            //    return tx;
            //}
        }

        public void UnCommitUpdate<T>(T obj) where T : Entity
        {
            //using (ITransaction tx = _session.BeginTransaction())
            //{
            SetEntiyInfo(obj);
            _session.Update(obj);
            if (!_dic.ContainsValue(obj))
            {
                _dic.Add(Guid.NewGuid(), obj);
            }
            //_session.Flush();
            //    return tx;
            //}
        }

        /// <summary>
        /// 根据实体和状态编码判断是否存在状态在实体中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tclass"></param>
        /// <returns></returns>
        public bool HasStateCodeInEntity(Guid guid, string stateCode)
        {
            try
            {
                var state = (from list in _session.Query<State>()
                             where list.Tid.Equals(guid)
                             && list.StateCode.Equals(stateCode)
                             && list.DelFlag == false
                             select list).FirstOrDefault();

                return state != null;

            }
            catch (Exception ex)
            {
                var message = Log(ex, ServerName);
                throw new Exception(message);
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="tclass"></param>
        /// <param name="stateCode"></param>
        /// <param name="optTerm"></param>
        /// <param name="optUser"></param>
        /// <returns></returns>
        public void UpdateState<T>(T tclass, string stateCode, string optTerm, string optUser)
            where T : Entity
        {
            var str = stateCode.Split('.');
            var oldState = (from list in Session.Query<State>()
                            where list.Tid == tclass.ID
                                    && list.GroupCode == str[0]
                            select list).FirstOrDefault();
            if (oldState == null)
            {
                oldState = new State
                {
                    Tid = tclass.ID,
                    Name = typeof(T).ToString(),
                    GroupCode = str[0],
                    StateCode = stateCode,
                    UpdateTime = DateTime.Now,
                };
                Save(oldState);
            }
            else
            {
                if (oldState.StateCode != stateCode)
                {
                    oldState.StateCode = stateCode;
                    Update(oldState);
                }
            }
         
        }

        /// <summary>
        /// 根据实体和组编码获取该实体该组状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tclass"></param>
        /// <returns></returns>
        public State GetStateByEntityAndGroupCode(Guid guid, string groupCode)
        {
            try
            {
                var statelist = (from list in _session.Query<State>()
                                 where list.Tid.Equals(guid)
                                 && list.GroupCode == groupCode
                                 && list.DelFlag == false
                                 select list).FirstOrDefault();

                try
                {
                    var str = statelist.StateCode;
                }
                catch
                {
                    string message = String.Format("{0}-{1}:{2}", ServerName, @"GetStateByEntityAndGroupCode",
                                                   "获取该组状态发生异常，请联系管理员");
                    Logger.Write(message);
                    throw new Exception(message);
                }
                return statelist;

            }
            catch (Exception ex)
            {
                var message = Log(ex, ServerName);
                throw new Exception(message);
            }
        }

        #region 写日志

        protected string Log(Exception ex, string serverName)
        {
            string fun = new System.Diagnostics.StackFrame(1).GetMethod().Name;
            string message = String.Format("{0}-{1}:发生异常:{2}", serverName, fun, ex.Message);
            Logger.Write(message);
            return message;
        }
        #endregion
    }
}
