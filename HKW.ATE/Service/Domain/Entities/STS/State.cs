using System;
using System.ComponentModel.DataAnnotations;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.STS
{
    /// <summary>
    /// 状态表
    /// </summary>
    public class State : Entity 
    {
        /// <summary>
        /// 状态所属对象
        /// </summary>
        [Display(Name = "状态所属对象ID", Order = 101)]
        public virtual Guid Tid { get; set; }
        /// <summary>
        /// 所属对象名称
        /// </summary>
        [Display(Name = "所属对象名称",Order = 102)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 状态组
        /// </summary>
        [Display(Name = "状态组",Order = 103)]
        public virtual string GroupCode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [RoundtripOriginal]
        [Display(Name = "状态",Order = 104)]
        public virtual string StateCode { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间",Order = 105)]
        public virtual DateTime? UpdateTime { get; set; }
    }
}
