using System.Linq;
using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.DaoFundation;
using NHibernate.Linq;

namespace HKW.Practices.Domain.Dao
{
    public class SubSystemDao : BaseDao
    {
        public IQueryable<SubSystem> GetSubSystems()
        {
            using (var session = SessionProvider.GetCurrentSession())
            {
                return from a in session.Query<SubSystem>()
                       where !a.DelFlag
                       select a;
            }
        }
    }
}
