using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.Entities.SYS
{
    public class EnumHeader : Entity
    {
        [Required]
        [Display(Name = "分组", Order = 0, Description = "枚举的分组")]
        public virtual string HeaderGroup { get; set; }

        [Required]
        [Display(Name = "编码", Order = 1, Description = "枚举的编码")]
        public virtual string Code { get; set; }

        [Required]
        [Display(Name = "名称", Order = 2, Description = "枚举的名称")]
        public virtual string Name { get; set; }

        public virtual IList<EnumDetail> EnumDetails { get; set; }
    }
}
