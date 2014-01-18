using System;
using System.Configuration;

namespace HKW.Practices.NHBase.DaoFundation
{
    public class Config
    {
        #region 私有成员

        private static readonly object Locker = new object();
        private static string _sessionSourceType;
        private static string _httpSessionSourceItemName;
#pragma warning disable 649
        private static bool? _userSessionSource;
#pragma warning restore 649
        private static string _domainAssembly;
        private static string _localConnectionstringPath;

        #endregion

        #region 属性

        /// <summary>
        /// Session资源源类型;http,threadStatic
        /// </summary>
        public static string SessionSourceType
        {
            get
            {
                lock (Locker)
                {
                    return _sessionSourceType ??
                           (_sessionSourceType = ConfigurationManager.AppSettings["SessionSourceType"]);
                }
            }
        }

        /// <summary>
        /// HttpSessionSource存放HttpContext.Current.Items的键值名
        /// </summary>
        public static string HttpSessionSourceItemName
        {
            get
            {
                lock (Locker)
                {
                    return _httpSessionSourceItemName ??
                           (_httpSessionSourceItemName = ConfigurationManager.AppSettings["HttpSessionSourceItemName"]);
                }
            }

        }

        /// <summary>
        /// 是否使用Session资源源
        /// </summary>
        public static bool UserSessionSource
        {
            get
            {
                lock (Locker)
                {
                    return _userSessionSource ?? Convert.ToBoolean(ConfigurationManager.AppSettings["UserSessionSource"]);
                }
            }
        }

        public static string DomainAssembly
        {
            get
            {
                lock (Locker)
                {
                    return _domainAssembly ?? (_domainAssembly = ConfigurationManager.AppSettings["DomainAssembly"]);
                }
            }
        }

        public static string LocalConnectionStringPath
        {
            get
            {
                lock (Locker)
                {
                    return _localConnectionstringPath ?? (_localConnectionstringPath = ConfigurationManager.AppSettings["LocalConnectionStringPath"]);
                }
            }
        }

        #endregion
    }
}
