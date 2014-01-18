using System;
using HKW.ATE.Domain.Entities.SYS;
using HKW.ATE.Domain.Enum;
using HKW.Practices.NHBase.DaoFundation;
using NUnit.Framework;

namespace HKW.ATE.Domain.Test.System
{
    [TestFixture]
    public class TestInitData
    {
        private readonly BaseDao _dao = new BaseDao();

        [Test]
        public void TestInitAllData()
        {
            TestAddUser();
            TestAddRole();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        [Test]
        public void TestAddUser()
        {
            var user = new User
            {
                UserName = "Test0",
                LoginName = "Test0",
                Password = "123456",
                StartDate = DateTime.Now,
                Email = "Test0@yahoo.com",
                UserType = UserTypeEnum.StationBoy,
                BelongID = SysBelongId.AdministratorBelongId
            };
            _dao.Save(user);

            user = new User
            {
                UserName = "杨涛",
                LoginName = "yangt",
                Password = "123456",
                StartDate = DateTime.Now,
                Email = "Test0@yahoo.com",
                UserType = UserTypeEnum.StationBoy,
                BelongID = SysBelongId.AdministratorBelongId,
                UserRoles = "系统管理员"
            };
            _dao.Save(user);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        [Test]
        public void TestAddRole()
        {
            var role = new Role
            {
                Name = "系统管理员",
                UserType = UserTypeEnum.StationBoy
            };
            _dao.Save(role);

            role = new Role
            {
                Name = "主管部门",
                UserType = UserTypeEnum.StationBoy
            };
            _dao.Save(role);
        }
    }
}
