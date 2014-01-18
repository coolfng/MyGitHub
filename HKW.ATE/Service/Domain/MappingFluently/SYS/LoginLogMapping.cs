using HKW.ATE.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.SYS
{
    public class LoginLogMapping : EntityMapping<LoginLog>
   {
        public LoginLogMapping()
        {
            ManyToOne(entity => entity.User, map => map.Column("User_ID"));
            Property(entity => entity.LoginTerm, map => map.Length(100));
            Property(entity => entity.LoginTime);
            Property(entity => entity.LogoutTime);
        }
    }
}
