using System.ComponentModel.DataAnnotations;
using HKW.ATE.Domain.Resources;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.STS
{
    /// <summary>
    /// 状态字典表
    /// </summary>
   public class StateDictionary:Entity
    {
       /// <summary>
        /// 对象名称
       /// </summary>
       [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
       [Display(Name = "对象名称",Order = 101)]
       public virtual string ClassName { get; set; }
       /// <summary>
       /// 对象中文名称
       /// </summary>
       [Display(Name = "对象中文名称",Order = 102)]
       public virtual string Name { get; set; }
       /// <summary>
       /// 状态组
       /// </summary>
       [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
       [Display(Name = "状态组",Order = 103)]
       public virtual string GroupCode { get; set; }
       /// <summary>
       /// 状态组名称
       /// </summary>
       [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
       [Display(Name = "状态组名称",Order = 104)]
       public virtual string GroupDescripe { get; set; }
       /// <summary>
       /// 状态
       /// </summary>
       [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
       //[UniqueName(UniqueType.StateCode)]
       [Display(Name = "状态",Order = 105)]
       public virtual string StateCode { get; set; }
       /// <summary>
       /// 状态名称
       /// </summary>
       [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
       //[UniqueName(UniqueType.StateDescripe)]
       [Display(Name = "状态名称",Order = 106)]
       public virtual string StateDescripe { get; set; }

       /// <summary>
       /// 是否为手工维护
       /// </summary>
       [Display(Name = "是否为手工维护", Order = 107)]
       public virtual bool IsWriten { get; set; }

       /// <summary>
       /// 是否为瞬时
       /// </summary>
       [Display(Name = "是否为瞬时", Order = 108)]
       public virtual bool IsTransient { get; set; } 
    }
}
