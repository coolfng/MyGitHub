using System;
using System.ComponentModel.DataAnnotations;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.RIAService.Web.Models.SYS
{
    public class AuthFunUnitModel : Entity
    {
        public AuthFunUnitModel()
        {
            IsEnable = true;
        }
        [Display(AutoGenerateField = false)]
        public int SubsystemSeq { get; set; }

        [Display(AutoGenerateField = false)]
        public int FunUnitSeq { get; set; }

        [Display(Name = "功能单元", Order = 3)]
        public string Name { get; set; }

        [Display(AutoGenerateField = false)]
        public Guid SubSystemID { get; set; }

        [Display(Name = "所属子系统", AutoGenerateField = false, Order = 1)]
        public string SubSystemName { get; set; }

        [Display(Name = "授权", Order = 4)]
        public bool Selected { get; set; }

        [Display(Name = "默认", Order = 2)]
        public bool IsDefault { get; set; }

        [Display(AutoGenerateField = false)]
        public string FunMainView { get; set; }

        [Display(AutoGenerateField = true)]
        public Guid RoleAuthId { get; set; }

        [Display(AutoGenerateField = false)]
        public Guid RoleId { get; set; }

        [Display(AutoGenerateField = false)]
        public bool IsEnable { get; set; }

    }
}
