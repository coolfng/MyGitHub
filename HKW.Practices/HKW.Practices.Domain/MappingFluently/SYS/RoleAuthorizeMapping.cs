using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.MappingFluently.SYS
{
    public class RoleAuthorizeMapping : EntityMapping<RoleAuthorize>
    {
        public RoleAuthorizeMapping()
        {
            ManyToOne(e => e.Role, map => map.Column("Role_ID"));
            ManyToOne(e => e.FunUnit, map => map.Column("FunUnit_ID")); 
        }
    }
}
