using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using HKW.ATE.Domain.Dao.SYS;
using HKW.ATE.Domain.Entities.SYS;
using HKW.ATE.RIAService.Web.Models.SYS;

namespace HKW.ATE.RIAService.Web.System
{
    [EnableClientAccess]
    public class SystemManageService : BaseService
    {
        #region Query
        [Query]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IQueryable<FunUnit> GetFunUnits()
        {
            var r = _dao.GetObjects<FunUnit>();
            return r;
        }

        [Query]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IQueryable<SubSystem> GetSubSystems()
        {
            return _dao.GetSubSystems();
        }

        [Query]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IEnumerable<AuthSubSystemModel> GetAuthSubSystems()
        {
            var roelAuths = new List<RoleAuthorize>();

            using (var dao = new UsersManageDao())
            {
                var user = dao.GetUserbyLoginName(ServiceContext.User.Identity.Name);
                if (user != null)
                {
                    foreach (var role in user.Roles)
                    {
                        roelAuths.AddRange(dao.GetRoleAuthorizebyRoleName(role));
                    }
                }
            }

            var r = new List<AuthSubSystemModel>();
            foreach (var subsystem in _dao.GetSubSystems())
            {
                if (!subsystem.DelFlag)
                {
                    var s = new AuthSubSystemModel { ID = subsystem.ID, Icon = subsystem.Icon, Name = subsystem.Name };
                    foreach (var f in subsystem.FunUnits)
                    {
                        if (!f.DelFlag)
                        {
                            var auth = (from a in roelAuths
                                        where a.NhFunUnit.Equals(f)
                                        select a).FirstOrDefault();
                            if (auth != null)
                            {
                                var af = new AuthFunUnitModel { ID = f.ID, SubSystemID = f.SubSystemID, Name = f.Name, FunMainView = f.FunMainView, IsEnable = true };
                                s.FunUnits.Add(af);
                            }
                        }
                    }
                    if (s.FunUnits.Count > 0)
                    {
                        r.Add(s);
                    }
                }
            }
            return r;
        }

        [Query]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public IEnumerable<AuthFunUnitModel> GetAuthFunUnits(Guid id)
        {
            return _dao.GetFunUnits(id).Select(f => new AuthFunUnitModel {ID = f.ID, SubSystemID = f.SubSystemID, Name = f.Name, FunMainView = f.FunMainView}).ToList();
        }

        #endregion

        #region CUD+  SubSystem
        [Insert]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void InertSubSystem(SubSystem subSystem)
        {
            _dao.Save(subSystem);
            SyncID(subSystem);
        }

        [Update]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void UpdateSubSystem(SubSystem subSystem)
        {
            _dao.SaveOrUpdate(subSystem);
            SyncID(subSystem);
        }

        private void SyncID(SubSystem subSystem)
        {
            foreach (FunUnit detail in ChangeSet.GetAssociatedChanges(subSystem, o => o.FunUnits))
            {
                ChangeOperation op = ChangeSet.GetChangeOperation(detail);
                switch (op)
                {
                    case ChangeOperation.Insert:
                        detail.SubSystemID = subSystem.ID;
                        break;
                }
            }
        }

        [Delete]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DeleteSubSystem(SubSystem subSystem)
        {
            //_dao.DeleteSubSystem(subSystem);
            _dao.Delete(subSystem);
        }

        #endregion CUD+

        #region CUD+  FunUnit

        [Insert]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void InertFunUnit(FunUnit funUnit)
        {
            _dao.Save(funUnit);
        }

        [Update]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void UpdateFunUnit(FunUnit funUnit)
        {
            if (funUnit.SubSystemID.Equals(new Guid()))
            {
                _dao.Delete(funUnit);
            }
            else
            {
                _dao.SaveOrUpdate(funUnit);
            }
        }

        [Delete]
        [RequiresAuthentication(ErrorMessage = UnloginErrMsg)]
        public void DeleteFunUnit(FunUnit funUnit)
        {

            _dao.DeleteFunUnit(funUnit);
        }

        #endregion CUD+

        #region Infrastructure

        public SystemManageService()
        {
            _dao = new SystemManageDao();
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

        private readonly SystemManageDao _dao;

        #endregion Infrastructure
    }
}
