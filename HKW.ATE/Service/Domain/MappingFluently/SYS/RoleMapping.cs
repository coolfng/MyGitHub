using HKW.ATE.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.SYS
{
    public class RoleMapping : EntityMapping<Role>
    {
        public RoleMapping()
        {
            Property(e => e.Name, map => map.Length(100));
            Property(e => e.DefaultView, map => map.Length(100));
            Property(e => e.UserType);
        }
    }
}
