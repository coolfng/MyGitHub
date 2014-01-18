using System;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.SYS
{
    public class ManageRegion : Entity
    {
        public virtual Guid UserId { get; set; }

        public virtual Guid LocationId { get; set; }
    }
}
