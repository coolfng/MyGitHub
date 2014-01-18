using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.Entities.SYS
{
    /// <summary>
    /// 子系统
    /// </summary>
    public class SubSystem : Entity
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public virtual int Seq { get; set; }
    }
}
