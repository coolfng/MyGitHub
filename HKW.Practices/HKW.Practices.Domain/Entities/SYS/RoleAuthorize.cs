using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.Entities.SYS
{
    public class RoleAuthorize : Entity
    {
        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 功能模块
        /// </summary>
        public virtual FunUnit FunUnit { get; set; }
    }
}
