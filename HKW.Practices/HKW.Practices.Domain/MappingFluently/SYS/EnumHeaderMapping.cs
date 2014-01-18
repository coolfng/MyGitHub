using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;
using NHibernate.Mapping.ByCode;

namespace HKW.Practices.Domain.MappingFluently.SYS
{
    public class EnumHeaderMapping : EntityMapping<EnumHeader>
    {
        public EnumHeaderMapping()
        {
            Property(e => e.HeaderGroup, map => map.Length(100));
            Property(e => e.Name);
            Property(e => e.Code);
            Bag(e => e.EnumDetails,
                m => m.Key(e => e.Column("EnumHeader_ID")),
                m => m.OneToMany(e => e.Class(typeof(EnumDetail)))
                );
            Bag(e => e.EnumDetails, e => e.Lazy(CollectionLazy.NoLazy));
        }
    }
}
