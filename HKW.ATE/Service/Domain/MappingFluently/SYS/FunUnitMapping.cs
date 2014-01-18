using HKW.ATE.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.SYS
{
    public class FunUnitMapping : EntityMapping<FunUnit>
    {
        public FunUnitMapping()
        {
            Property(e => e.Name, map => map.Length(100));
            Property(e => e.SubSystemID); 
            Property(e => e.Seq, map => map.Length(10));
            Property(e => e.FunMainView, map => map.Length(50));
        }
    }
}
