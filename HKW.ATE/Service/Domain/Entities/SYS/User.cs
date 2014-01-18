using System;
using System.ComponentModel.DataAnnotations;
using HKW.ATE.Domain.Enum;
using HKW.ATE.Domain.Resources;
using HKW.ATE.Domain.Validation;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.SYS
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class User : Entity
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        [Key]
        public virtual string Name
        {
            get { return ID.ToString(); }
            set
            {
                
            }
        }

        [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
        [UniqueName(UniqueType.UserName)]
        [Display(Name = "姓名", Description =  "用户的名称。")]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
        [RoundtripOriginal]
        [UniqueName(UniqueType.LoginName)]
        [Display(Name = "用户名", Description = "系统登录时将使用。")]
        public virtual string LoginName { get; set; }

        /// <summary>
        /// 口令
        /// </summary>
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// 删除日期
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "请输入正确的Email格式")]
        public virtual string Email { get; set; }

        /// <summary>
        /// 所属单位ID
        /// </summary>
        /// <remarks>
        /// <para>用户类型为企业:企业ID</para>
        /// <para>用户类型为物业:物业ID</para>
        /// <para>用户类型为产权单位:产权单位ID</para>
        /// <para>用户类型为管理员:静态ID</para>
        /// <para>用户类型为政府:静态ID</para>
        /// </remarks>
        public virtual Guid BelongID { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual UserTypeEnum UserType { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual string UserRoles { get; set; }

        public virtual string DefaultView { get; set; }

        public virtual Guid LoginToken { get; set; }
    }
}
