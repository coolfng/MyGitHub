using System;
using System.Collections.Generic;
using System.Linq;
using HKW.ATE.Domain.Entities.SYS;
using HKW.ATE.Domain.Enum;
using HKW.Practices.Public.Security;
using NHibernate.Linq;

namespace HKW.ATE.Domain.Dao.SYS
{
    public class UsersManageDao : ExtendDao
    {
        public IQueryable<User> GetUserbyBelongAndActived(Guid belongId, bool actived)
        {
            return from a in Session.Query<User>()
                   where a.BelongID.Equals(belongId) && a.DelFlag == !actived
                   orderby a.UserName
                   select a;
        }

        public IQueryable<Role> GetRolesbyUserType(UserTypeEnum usertype)
        {
            return from a in Session.Query<Role>()
                   where a.UserType == usertype
                   orderby a.Name
                   select a;
        }

        public IQueryable<Role> GetRolesbyUserType(UserTypeEnum usertype, bool actived)
        {
            return from a in Session.Query<Role>()
                   where a.UserType == usertype && a.DelFlag != actived
                   orderby a.Name
                   select a;
        }

        public bool ValidateUser(string loginname, string password)
        {
            password = SecurityFunc.ConvertStrSecrt(loginname, password);
            var r = (from a in Session.Query<User>()
                     where a.LoginName.Equals(loginname) && a.Password.Equals(password) && !a.DelFlag
                     select a).FirstOrDefault();
            return r != null;
        }

        public User GetUserbyLoginName(string loginname)
        {
            var user = (from a in Session.Query<User>()
                        where a.LoginName.Equals(loginname)
                        select a).FirstOrDefault();
            if (user != null)
            {
                var defaultView = (from a in Session.Query<RoleAuthorize>()
                                   where user.UserRoles.Contains(a.NhRole.Name) && a.IsDefaultView
                                   select a).FirstOrDefault();
                if (defaultView != null)
                {
                    user.DefaultView = defaultView.NhFunUnit.FunMainView;
                }
            }
            return user;
        }

        public Dictionary<Guid, LoginRecord> GetLoginRecords()
        {
            var dic = new Dictionary<Guid, LoginRecord>();
            try
            {
                var list = (from a in _session.Query<LoginRecord>()
                            select a).ToList();
                foreach (var loginRecord in list)
                {
                    dic.Add(loginRecord.ID, loginRecord);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dic;
        }

        public IList<RoleAuthorize> GetRoleAuthorizebyRoleName(string rolename)
        {
            return (from a in Session.Query<RoleAuthorize>()
                    where a.NhRole.Name.Equals(rolename)
                    select a).ToList();
        }
        public IList<RoleAuthorize> GetRoleAuthorizebyRoleID(Guid roleid)
        {
            return (from a in Session.Query<RoleAuthorize>()
                    where a.NhRole.ID.Equals(roleid)
                    select a).ToList();
        }
    }
}
