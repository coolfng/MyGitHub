using System.Linq;
using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.DaoFundation;
using NHibernate.Linq;

namespace HKW.Practices.Domain.Dao
{
    public class EnumManageDao : BaseDao
    {
        public IQueryable<EnumHeader> GetEnumHeaders()
        {
            return from obj in Session.Query<EnumHeader>()
                   select obj;
        }
    }
}
