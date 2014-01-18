using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.MappingFluently.SYS
{
    public class SubSystemMapping : EntityMapping<SubSystem>
    {
        public SubSystemMapping()
        {
            Property(e => e.Name, map => map.Length(100));
            Property(e => e.Seq);
        }
    }
}
