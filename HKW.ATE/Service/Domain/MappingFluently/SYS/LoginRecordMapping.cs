using HKW.ATE.Domain.Entities.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.MappingFluently.SYS
{
    public class LoginRecordMapping : EntityMapping<LoginRecord>
   {
        public LoginRecordMapping()
        {
            ManyToOne(entity => entity.User, map => map.Column("User_ID"));
            Property(entity => entity.Role, map => map.Length(100));
            Property(entity=>entity.UID,map=>map.Length(100));
            Property(entity => entity.ClientSN, map => map.Length(100));
            Property(entity => entity.ClientEndpoint, map => map.Length(100));
            Property(entity => entity.LastTime);
            Property(entity => entity.Nettype);
        }
    }
}
