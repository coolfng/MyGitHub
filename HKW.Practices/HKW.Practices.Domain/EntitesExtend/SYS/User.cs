using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel.DomainServices.Server.ApplicationServices;
using System.Text;

namespace HKW.Practices.Domain.Entities.SYS
{
    /// <summary>
    /// 用户（扩展类）
    /// </summary>
    public partial class User :  IUser, IPrincipal, IIdentity
    {
        #region 实现接口部分
        /// <summary>
        /// 角色列表
        /// </summary>
        public IEnumerable<string> Roles {
            get 
            {
                return (from userAuthoriz in UserAuthorizes where !userAuthoriz.Role.DelFlag select userAuthoriz.Role.Name).ToArray();
            }
            set
            {
                
            }
        }

        /// <summary>
        /// 用户的标识
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// 是否属于指定的角色
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            if (this.Roles == null)
            {
                return false;
            }
            return this.Roles.Contains<string>(role);

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

        /// <summary>
        /// 用户是否验证了
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(this.Name);
            }
        }

        #endregion
    }
}
