using System;
using System.ComponentModel.DataAnnotations;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.SYS
{
    /// <summary>
    /// 功能单元
    /// </summary>
    public class FunUnit : Entity
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        [Display(Name = "功能名称", Order = 101)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 页面路径
        /// </summary>
        [Display(Name = "页面路径", Order = 102)]
        public virtual string FunMainView { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Display(Name = "序号", Order = 100)]
        public virtual int Seq { get; set; }

        [Display(AutoGenerateField = false)]
        public virtual Guid SubSystemID { get; set; }
    }
}
