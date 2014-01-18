using System;
using System.Linq;
using HKW.ATE.Domain.Entities.SYS;
using HKW.Practices.NHBase.DaoFundation;
using NHibernate.Linq;

namespace HKW.ATE.Domain.Dao.SYS
{
    public class SystemManageDao : BaseDao
    {
        public IQueryable<SubSystem> GetSubSystems()
        {
            var subsystems = from a in Session.Query<SubSystem>()
                             orderby a.Seq
                             select a;
            foreach (var subsystem in subsystems)
            {
                subsystem.LoadFunUnits();
            }
            return subsystems;
        }

        public IQueryable<FunUnit> GetFunUnits(Guid id)
        {
            var funUnits = from a in Session.Query<FunUnit>()
                           where a.SubSystemID.Equals(id)
                           orderby a.Seq
                           select a;
            return funUnits;
        }

        public void DeleteSubSystem(SubSystem subsystem)
        {
            var h = Get<SubSystem>(subsystem.ID);
            foreach (var detail in h.FunUnits)
            {
                Delete(detail);
            }
            Delete(h);
        }

        public void DeleteFunUnit(FunUnit funUnit)
        {
            var delfunUnit = Get<FunUnit>(funUnit.ID);
            if (delfunUnit != null)
            {
                var roleAuths = from a in Session.Query<RoleAuthorize>()
                                where a.NhFunUnit.ID.Equals(delfunUnit.ID)
                                select a;
                foreach (var auth in roleAuths)
                {
                    Delete(auth);
                }
                Delete(delfunUnit);
            }

        }

    }
}
