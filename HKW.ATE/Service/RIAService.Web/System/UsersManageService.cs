using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using HKW.ATE.Domain.Dao.SYS;
using HKW.ATE.Domain.Entities.SYS;
using HKW.ATE.Domain.Enum;
using HKW.ATE.RIAService.Web.Models.SYS;
using HKW.Practices.Public.Security;

namespace HKW.ATE.RIAService.Web.System
{
    [EnableClientAccess]
    public class UsersManageService : BaseService
    {
        #region Query
        [Query]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IQueryable<User> GetUsers(Guid belongId, bool actived)
        {
            return _dao.GetUserbyBelongAndActived(belongId, actived);
        }

        [Query]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IQueryable<Role> GetRoles(UserTypeEnum userType)
        {
            return _dao.GetRolesbyUserType(userType, true);
        }

        [Query]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IQueryable<Role> GetRolesbyUserType(UserTypeEnum userType, bool actived)
        {
            return _dao.GetRolesbyUserType(userType, actived);
        }

        [Query]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IEnumerable<AuthFunUnitModel> GetFunUintsforAuth(Guid id)
        {
            var roelAuths = _dao.GetRoleAuthorizebyRoleID(id);
            using (var dao = new SystemManageDao())
            {
                var r = new List<AuthFunUnitModel>();
                foreach (var subsystem in dao.GetSubSystems())
                {
                    foreach (var f in subsystem.FunUnits)
                    {
                        var af = new AuthFunUnitModel
                        {
                            ID = f.ID,
                            SubSystemID = f.SubSystemID,
                            SubSystemName = subsystem.Name,
                            Name = f.Name,
                            DelFlag = f.DelFlag,
                            SubsystemSeq = subsystem.Seq,
                            FunUnitSeq = f.Seq,
                            RoleId = id
                        };
                        var auth = (from a in roelAuths
                                    where a.NhFunUnit.ID.Equals(af.ID)
                                    select a).FirstOrDefault();
                        if (auth != null)
                        {
                            af.Selected = true;
                            af.IsDefault = auth.IsDefaultView;
                            af.RoleAuthId = auth.ID;
                        }
                        r.Add(af);
                    }
                }
                return r;
            }
        }

        [Query]
           [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IEnumerable<ChangePasswordModel> GetChangePasswordModel()
        {
            return null;
        }

        #endregion

        #region CUD+  User
        [Insert]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void InertUser(User user)
        {
            user.StartDate = DateTime.Now;
            user.Password = SecurityFunc.ConvertStrSecrt(user.LoginName, "123456");
            _dao.Save(user);
        }

        [Update]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void UpdateUser(User user)
        {
            var oldUser = ChangeSet.GetOriginal(user);
            if (!oldUser.LoginName.Equals(user.LoginName))
            {
                user.Password = SecurityFunc.ConvertStrSecrt(user.LoginName, "123456");
            }
            _dao.SaveOrUpdate(user);
        }

        [Delete]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DeleteUser(User user)
        {
            _dao.Delete(user);
        }

        [Update(UsingCustomMethod = true)]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void ResetPassword(User user)
        {
            user.Password = SecurityFunc.ConvertStrSecrt(user.LoginName, "123456");
            _dao.SaveOrUpdate(user);
        }

        [Update(UsingCustomMethod = true)]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DisableUser(User user)
        {
            user.EndDate = DateTime.Now;
            user.DelFlag = true;
            _dao.SaveOrUpdate(user);
        }

        [Update(UsingCustomMethod = true)]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void EnableUser(User user)
        {
            user.EndDate = null;
            user.DelFlag = false;
            _dao.SaveOrUpdate(user);
        }
        #endregion CUD+

        #region CUD+  Role
        [Insert]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void InertRole(Role role)
        {
            _dao.Save(role);
        }

        [Update]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void UpdateRole(Role role)
        {
            _dao.SaveOrUpdate(role);
        }

        [Delete]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DeleteRole(Role role)
        {
            _dao.Delete(role);
        }

        [Update(UsingCustomMethod = true)]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DisableRole(Role role)
        {
            role.DelFlag = true;
            _dao.SaveOrUpdate(role);
        }

        [Update(UsingCustomMethod = true)]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void EnableRole(Role role)
        {
            role.DelFlag = false;
            _dao.SaveOrUpdate(role);
        }
        #endregion CUD+

        #region CUD+  AuthFunUnit
        [Insert]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void InertAuthFunUnit(AuthFunUnitModel authFunUnit)
        {

        }

        [Update]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void UpdateAuthFunUnit(AuthFunUnitModel authFunUnit)
        {
            if (authFunUnit.Selected)
            {
                var roleauth = authFunUnit.RoleAuthId == new Guid() ? new RoleAuthorize() : _dao.Get<RoleAuthorize>(authFunUnit.RoleAuthId);
                roleauth.NhRole = _dao.Get<Role>(authFunUnit.RoleId);
                roleauth.NhFunUnit = _dao.Get<FunUnit>(authFunUnit.ID);
                roleauth.IsDefaultView = authFunUnit.IsDefault;
                _dao.SaveOrUpdate(roleauth);
            }
            else
            {
                var roleauth = _dao.Get<RoleAuthorize>(authFunUnit.RoleAuthId);
                if (roleauth != null)
                {
                    _dao.Delete(roleauth);
                }
            }

        }

        [Delete]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DeleteAuthFunUnit(AuthFunUnitModel authFunUnit)
        {

        }
        #endregion CUD+

        #region CUD+  ChangePasswordModel
        [Insert]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void InertChangePasswordModel(ChangePasswordModel changePasswordModel)
        {
            var user = _dao.GetUserbyLoginName(changePasswordModel.LoginName);
            user.Password = SecurityFunc.ConvertStrSecrt(user.LoginName, changePasswordModel.NewPassword);
            _dao.SaveOrUpdate(user);
        }

        [Update]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void UpdateChangePasswordModel(ChangePasswordModel changePasswordModel)
        {

        }

        [Delete]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DeleteChangePasswordModel(ChangePasswordModel changePasswordModel)
        {

        }
        #endregion CUD+

        #region CUD+  ManageRegion
        [Insert]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void InertManageRegion(ManageRegion manageRegion)
        {
            if (manageRegion.UserId!=new Guid())
            {
                 _dao.Save(manageRegion);
            }
        }

        [Update]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void UpdateManageRegion(ManageRegion manageRegion)
        {
            _dao.Merge(manageRegion);
        }

        [Delete]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DeleteManageRegion(ManageRegion manageRegion)
        {
            _dao.Delete(manageRegion);
        }
        #endregion CUD+

        #region Invoke User

        [Invoke]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public bool ChangePassword(string password)
        {
            return true;
        }

        #endregion

        #region Infrastructure

        public UsersManageService()
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
            else
            {
                return false;
            }
        }

        private readonly UsersManageDao _dao;

        #endregion Infrastructure
    }
}
