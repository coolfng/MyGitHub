using System;
using System.ComponentModel.DataAnnotations;
using HKW.ATE.Domain.Dao.SYS;

namespace HKW.ATE.RIAService.Web.Models.SYS
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CheckOldPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var oldpassword = value as string;
            var model = validationContext.ObjectInstance as ChangePasswordModel;
            using (var dao = new UsersManageDao())
            {
                if (dao.ValidateUser(model.LoginName, oldpassword))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("旧口令错误", new[] { validationContext.MemberName });
            }
        }
    }
}
