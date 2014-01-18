using System;
using System.ComponentModel.DataAnnotations;
using HKW.ATE.Domain.Resources;

namespace HKW.ATE.RIAService.Web.Models.SYS
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CheckNewPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as ChangePasswordModel;
            if (model.NewPassword != null)
                if (model.NewPassword.Equals(model.ConfirmPassword))
                {
                    return ValidationResult.Success;
                }
            return new ValidationResult(ValidationErrorResources.PasswordConfirmationMismatchErrMsg, new[] { validationContext.MemberName });
        }
    }
}
