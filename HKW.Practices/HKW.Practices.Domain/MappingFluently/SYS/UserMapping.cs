using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;
using NHibernate.Mapping.ByCode;

namespace HKW.Practices.Domain.MappingFluently.SYS
{
    public class UserMapping : EntityMapping<User>
    {
        public UserMapping()
        {
            Property(e => e.Name, map => map.Length(100));
            Property(e => e.LogName, map => map.Length(100));
            Property(e => e.Password, map => map.Length(100));
            Property(e => e.StartDate);
            Property(e => e.EndDate);
            Bag(e => e.UserAuthorizes,
                m => m.Key(e => e.Column("User_ID")),
                m => m.OneToMany(e => e.Class(typeof(UserAuthorize)))
                );
            Bag(e => e.UserAuthorizes, e => e.Lazy(CollectionLazy.NoLazy));
        }
    }
}
