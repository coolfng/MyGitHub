using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HKW.Practices.NHBase.DaoFundation
{
    public class SessionStorageFactory
    {
        /// <summary>
        /// SessionSourceType 的值为 http 或 threadStatic
        /// </summary>
        /// <returns></returns>
        public static ISessionStorage GetSessionStorage()
        {
            if (Config.SessionSourceType == "http")  
            {
                return new HttpSessionStorage();
            }
            else
            {
                return new ThreadSessionStorage();
            }
        }
    }
}
