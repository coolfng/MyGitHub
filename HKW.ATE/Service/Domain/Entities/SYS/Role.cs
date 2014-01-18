using System.ComponentModel.DataAnnotations;
using HKW.ATE.Domain.Enum;
using HKW.ATE.Domain.Resources;
using HKW.ATE.Domain.Validation;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.SYS
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
        [UniqueName(UniqueType.RoleName)]
        [Display(Name = "角色名称")]
        public virtual string Name { get; set; }

        public virtual string DefaultView { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
        public virtual UserTypeEnum UserType { get; set; }
    }
}
