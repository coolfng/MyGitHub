using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.MappingFluently.SYS
{
    public class UserAuthorizeMapping : EntityMapping<UserAuthorize>
    {
        public UserAuthorizeMapping()
        {
            ManyToOne(e => e.Role, map => map.Column("Role_ID"));
            ManyToOne(e => e.User, map => map.Column("User_ID"));
        }
    }
}
