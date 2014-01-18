using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.ServiceModel.DomainServices.Server;
using HKW.ATE.Domain.Dao.SYS;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.Domain.Entities.SYS
{
    /// <summary>
    /// 子系统
    /// </summary>
    public class SubSystem : Entity
    {
        public SubSystem()
        {
            FunUnits = new EntitySet<FunUnit>();
        }

        public void LoadFunUnits()
        {
            using (var dao = new SystemManageDao())
            {
                FunUnits = dao.GetFunUnits(ID).ToEntitySet();
            }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        [Required]
        [Display(Name = "系统名称", Order = 101)]
        public virtual string Name { get; set; }

        [Display(Name = "图标", Order = 101)]
        public virtual string Icon { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Display(Name = "序号", Order = 100)]
        public virtual int Seq { get; set; }

        [Include]
        [Composition]
        [Association("SubsytemFunUnit", "ID", "SubSystemID")]
        [Display(AutoGenerateField = false)]
        public EntitySet<FunUnit> FunUnits { get; set; }
    }
}
