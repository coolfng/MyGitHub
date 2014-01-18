using HKW.ATE.Domain.Dao.SYS;
using HKW.ATE.Domain.Entities.SYS;
using HKW.ATE.Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace HKW.ATE.Domain.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class UniqueNameAttribute : ValidationAttribute
    {
        private readonly UniqueType _param;

        public UniqueNameAttribute(UniqueType unique)
        {
            _param = unique;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;
            switch (_param)
            {
                case UniqueType.UserName:
                    result = ValidateUserName(value, validationContext);
                    break;
                case UniqueType.LoginName:
                    result = ValidateLoginName(value, validationContext);
                    break;
                case UniqueType.RoleName:
                    result = ValidateRoleName(value, validationContext);
                    break;
            }
            return result;
        }

        private ValidationResult ValidateUserName(object value, ValidationContext validationContext)
        {
            var name = value as string;
            var user = validationContext.ObjectInstance as User;
            using (var dao = new ConfirmUniqueDao())
            {
                return FormatResult(dao.ConfirmUserNameUnipue(name, user.ID), name, validationContext);
            }
        }

        private ValidationResult ValidateLoginName(object value, ValidationContext validationContext)
        {
            var name = value as string;
            var user = validationContext.ObjectInstance as User;
            using (var dao = new ConfirmUniqueDao())
            {
                return FormatResult(dao.ConfirmLoginNameUnipue(name, user.ID), name, validationContext);
            }
        }

        private ValidationResult ValidateRoleName(object value, ValidationContext validationContext)
        {
            var name = value as string;
            var role = validationContext.ObjectInstance as Role;
            using (var dao = new ConfirmUniqueDao())
            {
                return FormatResult(dao.ConfirmRoleNameUnipue(name, role.ID, role.UserType), name, validationContext);
            }
        }

        private ValidationResult FormatResult(bool result, string value, ValidationContext validationContext)
        {
            if (result)
                return ValidationResult.Success;
            return new ValidationResult(string.Format(ValidationErrorResources.UniqueNameErrMsg, value, validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }

    public enum UniqueType
    {
        UserName,
        LoginName,
        RoleName
    }
}
