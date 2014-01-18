using System.ComponentModel.DataAnnotations;

namespace HKW.ATE.RIAService.Web.Models.SYS
{
    public class VersionModel
    {
        /// <summary>
        /// 最新版本号
        /// </summary>
        [Key]
        [Display(AutoGenerateField = false)]
        public string VersionNumber { get; set; }

        /// <summary>
        /// 版本内容
        /// </summary>
        [Display(AutoGenerateField = false)]
        public string VersionInfo { get; set; }

    }
}
