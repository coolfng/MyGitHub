using System.ComponentModel.DataAnnotations;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.Entities.SYS
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
    }
}
