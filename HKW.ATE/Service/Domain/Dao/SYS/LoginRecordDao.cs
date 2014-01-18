using System;
using System.Linq;
using HKW.ATE.Domain.Entities.SYS;
using NHibernate.Linq;

namespace HKW.ATE.Domain.Dao.SYS
{
    public class LoginRecordDao : ExtendDao
    {
        private const string ServerName = "LoginRecord";

        public LoginRecord GetLoginRecordById(Guid id)
        {
            try
            {
                return (from list in Session.Query<LoginRecord>()
                       where list.ID==id&&list.DelFlag==false
                       select list).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var message = Log(ex, ServerName);
                throw new Exception(message);
            }
        }
    }
}



