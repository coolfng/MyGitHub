using HKW.ATE.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.SYS
{
    public class ManageRegionMapping : EntityMapping<ManageRegion>
    {
        public ManageRegionMapping()
        {
            Property(e => e.UserId, map => map.Column("User_ID"));
            Property(e => e.LocationId, map => map.Column("Location_ID"));
        }
    }
}
