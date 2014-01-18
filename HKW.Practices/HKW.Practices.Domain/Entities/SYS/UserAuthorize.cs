using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.Entities.SYS
{
    public class UserAuthorize : Entity
    {
        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }
    }
}
