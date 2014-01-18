using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using HKW.Practices.NHBase.Domain;

namespace HKW.ATE.RIAService.Web.Models.SYS
{
    public class AuthSubSystemModel : Entity
    {
        private const string ICON_NORMAL = "/HKW.AMS.Module.SysFunc;component/Resources/icon/{0}.png";
        private const string ICON_HOVER = "/HKW.AMS.Module.SysFunc;component/Resources/icon/{0}_hover.png";
        private const string ICON_SELECTED = "/HKW.AMS.Module.SysFunc;component/Resources/icon/{0}_selected.png";
        private const string ICON_DISABLE = "/HKW.AMS.Module.SysFunc;component/Resources/icon/{0}_disable.png";

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        private string _icon;
        public string Icon
        {
            get
            {
                if (string.IsNullOrEmpty(_icon))
                {
                    return "defaultIcon";
                }
                return _icon;
            }
            set { _icon = value; }
        }

        public string IconNormal
        {
            get { return string.Format(ICON_NORMAL, Icon); }
        }

        public string IconHover
        {
            get { return string.Format(ICON_HOVER, Icon); }
        }

        public string IconSelected
        {
            get { return string.Format(ICON_SELECTED, Icon); }
        }

        public string IconDisable
        {
            get { return string.Format(ICON_DISABLE, Icon); }
        }

        private IList<AuthFunUnitModel> _funUnits = new List<AuthFunUnitModel>();
        [Include]
        [Association("AuthSubSystemFunUnit", "ID", "SubSystemID")]
        [Display(AutoGenerateField = false)]
        public IList<AuthFunUnitModel> FunUnits
        {
            get { return _funUnits; }
            set { _funUnits = value; }
        }
    }
}
