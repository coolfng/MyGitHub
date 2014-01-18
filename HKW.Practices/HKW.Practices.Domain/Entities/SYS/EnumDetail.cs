using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.Entities.SYS
{
    public class EnumDetail : Entity
    {
        [Required]
        public virtual string Code { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual int Seq { get; set; }

        [Required]
        public virtual EnumHeader EnumHeader { get; set; }
    }
}
