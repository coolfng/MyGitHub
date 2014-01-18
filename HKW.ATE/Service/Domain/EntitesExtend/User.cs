using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Server.ApplicationServices;

// ReSharper disable CheckNamespace
namespace HKW.ATE.Domain.Entities.SYS
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// 用户（扩展类）
    /// </summary>
    public partial class User :  IUser  
    {
        #region 实现接口部分

        /// <summary>
        /// 角色列表
        /// </summary>
        public IEnumerable<string> Roles {
            get
            {
                if (UserRoles != null)
                {
                    var a = Enumerable.ToArray<string>(UserRoles.Split('、'));
                    return a.ToArray();
                }
                else
                {
                    var a = new List<string>();
                    return a.ToArray();
                }
            }
            set
            {
                
            }
        }

        /// <summary>
        /// 是否属于指定的角色
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            if (Roles == null)
            {
                return false;
            }
            return Roles.Contains(role);

        }

        /// <summary>
        /// 身份验证的类型
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
