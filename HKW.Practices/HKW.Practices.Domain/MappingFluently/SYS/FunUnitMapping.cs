using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.MappingFluently.SYS
{
    public class FunUnitMapping : EntityMapping<FunUnit>
    {
        public FunUnitMapping()
        {
            Property(e => e.Name, map => map.Length(100));
            ManyToOne(e => e.SubSystem, map => map.Column("SubSystem_ID")); 
            Property(e => e.Seq, map => map.Length(10));
            Property(e => e.FunMainView, map => map.Length(50));
        }
    }
}
