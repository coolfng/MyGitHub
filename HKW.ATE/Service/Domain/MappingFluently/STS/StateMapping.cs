using HKW.ATE.Domain.Entities.STS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.STS
{
    public class StateMapping : EntityMapping<State>
  {
        public StateMapping()
        {
            Property(entity=>entity.Tid);
            Property(entity => entity.Name,map=>map.Length(100));
            Property(entity => entity.GroupCode, map => map.Length(100));
            Property(entity => entity.StateCode, map => map.Length(100));
            Property(entity => entity.UpdateTime);
        }
  }
}
