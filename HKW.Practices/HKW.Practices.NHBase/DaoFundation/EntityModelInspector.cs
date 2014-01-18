using System.Reflection;
using NHibernate.Mapping.ByCode;

namespace HKW.Practices.NHBase.DaoFundation
{
    public class EntityModelInspector : ExplicitlyDeclaredModel
    {
        public override bool IsOneToMany(MemberInfo member)
        {
            if (IsBag(member))
            {
                return true;
            }
            return base.IsOneToMany(member);
        }
    }
}
