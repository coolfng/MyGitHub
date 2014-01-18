using HKW.ATE.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.SYS
{
    public class UserMapping : EntityMapping<User>
    {
        public UserMapping()
        {
            Property(e => e.UserName, map => map.Length(100));
            Property(e => e.LoginName, map => map.Length(100));
            Property(e => e.Password, map => map.Length(100));
            Property(e => e.Email, map => map.Length(100));
            Property(e => e.UserRoles, map => map.Length(200));
            Property(e => e.StartDate);
            Property(e => e.EndDate);
            Property(e => e.BelongID);
            Property(e => e.UserType);
        }
    }
}
