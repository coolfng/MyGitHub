using System.ComponentModel.DataAnnotations;
using HKW.ATE.Domain.Resources;

namespace HKW.ATE.RIAService.Web.Models.SYS
{
    public class ChangePasswordModel
    {
        [Key]
        public string LoginName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
        [CheckOldPassword]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "PasswordLenghtErrMsg")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "RequiredErrMasg")]
        [CheckNewPassword]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(ValidationErrorResources), ErrorMessageResourceName = "PasswordLenghtErrMsg")]
        public string ConfirmPassword { get; set; }
    }
}
