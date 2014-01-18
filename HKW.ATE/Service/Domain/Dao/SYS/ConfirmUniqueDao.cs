using System;
using System.Linq;
using HKW.ATE.Domain.Entities.SYS;
using HKW.ATE.Domain.Enum;
using NHibernate.Linq;

namespace HKW.ATE.Domain.Dao.SYS
{
    public class ConfirmUniqueDao : ExtendDao
    {
        public bool ConfirmLoginNameUnipue(string loginname, Guid originalUserid)
        {
            var user = (from a in Session.Query<User>()
                        where a.LoginName.Equals(loginname) && !a.ID.Equals(originalUserid)
                        select a).FirstOrDefault();
            return user == null;
        }

        public bool ConfirmUserNameUnipue(string username, Guid originalUserid)
        {
            var user = (from a in Session.Query<User>()
                        where a.UserName.Equals(username) && !a.ID.Equals(originalUserid)
                        select a).FirstOrDefault();
            return user == null;
        }
        public bool ConfirmRoleNameUnipue(string rolename, Guid originalRoleid, UserTypeEnum usertype)
        {
            var user = (from a in Session.Query<Role>()
                        where a.Name.Equals(rolename) && !(a.ID.Equals(originalRoleid) && a.UserType == usertype)
                        select a).FirstOrDefault();
            return user == null;
        }
    }
}
