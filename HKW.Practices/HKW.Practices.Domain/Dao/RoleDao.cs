using System.Linq;
using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.DaoFundation;
using NHibernate.Linq;

namespace HKW.Practices.Domain.Dao
{
    public class RoleDao : BaseDao
    {
        public IQueryable<Role> GetRoles()
        {
            return from obj in Session.Query<Role>()
                   select obj;
        }
    }
}

