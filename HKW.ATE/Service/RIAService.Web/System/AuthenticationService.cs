using System;
using System.Linq;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server.ApplicationServices;
using System.Web.Security;
using HKW.ATE.Domain.Dao.SYS;
using HKW.ATE.Domain.Entities.SYS;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HKW.ATE.RIAService.Web.System
{
    [EnableClientAccess]
    public class AuthenticationService : BaseService, IAuthentication<User>
    {
        private const string SERVER_NAME = "AuthenticationService";

        private static readonly User DefaultUser = new User
        {
            Name = string.Empty
        };

        public User Login(string loginname, string password, bool isPersistent, string customData)
        {
            try
            {
                if (_dao.ValidateUser(loginname, password))
                {
                    LogoutLog(loginname);
                    FormsAuthentication.SetAuthCookie(loginname, isPersistent);
                    var user = GetUser(loginname);
                    var log = new LoginLog
                    {
                        User = user,
                        LoginTime = DateTime.Now
                    };
                    _dao.Save(log);
                    user.LoginToken = log.ID;
                    return user;
                }
                Logger.Write(String.Format("登录失败：{0}", loginname));
                return null;
            }
            catch (Exception ex)
            {
                Logger.Write(String.Format("{0}-{1}:发生异常:{2}", SERVER_NAME, @"GetBillSubjects", ex.Message));
                throw;
            }
        }

        private void LogoutLog(string loginname)
        {
            var oldlogs = _dao.GetObjects<LoginLog>().Where(p => p.User.LoginName.Equals(loginname)).Where(p => p.LogoutTime == null);
            foreach (LoginLog oldlog in oldlogs)
            {
                oldlog.LogoutTime = DateTime.Now;
                _dao.Update(oldlog);
            }
        }

        public User GetUser()
        {
            if ((ServiceContext != null) &&
                (ServiceContext.User != null) &&
                ServiceContext.User.Identity.IsAuthenticated)
            {
                return GetUser(ServiceContext.User.Identity.Name);
            }
            return DefaultUser;
        }

        private User GetUser(string loginname)
        {
            return _dao.GetUserbyLoginName(loginname);
        }

        public User Logout()
        {
            if ((ServiceContext != null) &&
                (ServiceContext.User != null) &&
                ServiceContext.User.Identity.IsAuthenticated)
            {
                LogoutLog(ServiceContext.User.Identity.Name);
            }
            FormsAuthentication.SignOut();
            return DefaultUser;
        }

        public void UpdateUser(User user)
        {
            if ((ServiceContext.User == null) ||
                (ServiceContext.User.Identity == null) ||
                !string.Equals(ServiceContext.User.Identity.Name, user.LoginName, StringComparison.Ordinal))
            {
                throw new UnauthorizedAccessException("只有登录后才能修改用户信息。");
            }
            _dao.SaveOrUpdate(user);
        }

        #region Infrastructure

        public AuthenticationService()
        {
            _dao = new UsersManageDao();
        }

        protected override bool PersistChangeSet()
        {
            if (base.PersistChangeSet())
            {
                _dao.Flush();
                return true;
            }
            return false;
        }

        private readonly UsersManageDao _dao;

        #endregion Infrastructure
    }
}
