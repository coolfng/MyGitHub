using System;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.SYS
{
    public class LoginLog : Entity
    {
        public virtual User User { get; set; }

        public DateTime? LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public string LoginTerm { get; set; }
    }
}
