using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.Entities.SYS
{
    /// <summary>
    /// 功能单元
    /// </summary>
    public class FunUnit : Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        
        /// <summary>
        /// 所属子系统
        /// </summary>
        public virtual SubSystem SubSystem { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public virtual int Seq { get; set; }

        /// <summary>
        /// 主功能页面
        /// </summary>
        public virtual string FunMainView { get; set; }
    }
}
