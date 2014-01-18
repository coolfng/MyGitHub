using System;
using HKW.ATE.Domain.Enum;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.SYS
{
   public class LoginRecord:Entity
    {
        public LoginRecord()
        {
            LastTime = DateTime.Now;
        }
        public virtual User User { get; set; }
        public virtual string Role { get; set; }
        public virtual string UID { get; set; }
        public virtual DateTime LastTime { get; set; }
        public virtual string ClientSN { get; set; }
        public virtual NetWorkType Nettype { get; set; }
        public virtual string ClientEndpoint { get; set; }}
   
}
