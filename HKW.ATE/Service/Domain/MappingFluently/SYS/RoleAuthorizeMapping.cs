using HKW.ATE.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.SYS
{
    public class RoleAuthorizeMapping : EntityMapping<RoleAuthorize>
    {
        public RoleAuthorizeMapping()
        {
            ManyToOne(e => e.NhRole, map => map.Column("Role_ID"));
            ManyToOne(e => e.NhFunUnit, map => map.Column("FunUnit_ID"));
            Property(e => e.IsDefaultView);
        }
    }
}
