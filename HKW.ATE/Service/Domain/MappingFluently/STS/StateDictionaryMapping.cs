using HKW.ATE.Domain.Entities.STS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.STS
{
    public class StateDictionaryMapping : EntityMapping<StateDictionary>
   {
        public StateDictionaryMapping()
        {
            Property(entity => entity.ClassName, map => map.Length(100));
            Property(entity => entity.Name, map => map.Length(100));
            Property(entity => entity.GroupDescripe, map => map.Length(100));
            Property(entity => entity.GroupCode, map => map.Length(100));
            Property(entity => entity.StateCode, map => map.Length(100));
            Property(entity => entity.StateDescripe, map => map.Length(100));
            Property(entity => entity.IsWriten);
            Property(entity => entity.IsTransient);
        }
    }
}
