using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.MappingFluently.SYS
{
    public class RoleMapping : EntityMapping<Role>
    {
        public RoleMapping()
        {
            Property(e => e.Name, map => map.Length(100));
        }
    }
}
