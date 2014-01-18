using System;
using System.Collections.Generic;
using HKW.Practices.NHBase.Domain;

namespace HKW.Practices.Domain.Entities.SYS
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class User : Entity
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 登录名称
        /// </summary>
        public virtual string LogName { get; set; }

        /// <summary>
        /// 口令
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        /// 删除日期
        /// </summary>
        public virtual DateTime? EndDate { get; set; }

        /// <summary>
        /// 用户授权角色列表
        /// </summary>
        public virtual IList<UserAuthorize> UserAuthorizes { get; set; }
    }
}
