using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.SYS
{
    public class RoleAuthorize : Entity
    {
        public virtual Role NhRole { get; set; }

        public virtual FunUnit NhFunUnit { get; set; }

        public virtual bool IsDefaultView { get; set; }
    }
}
