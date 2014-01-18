using System.Linq;
using HKW.Practices.Domain.Dao;
using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.DaoFundation;
using NUnit.Framework;

namespace HKW.Practices.Dao.Test
{
    [TestFixture]
    public class TestUserManage
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            //  HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

        }

        [SetUp]
        public void SetupOne()
        {

        }

        [Test]
        public void TestRole()
        {
            using (var dao = new RoleDao())
            {
                var r = new Role { Name = "a1" };
                dao.Save(r);
                Assert.IsFalse(r.DelFlag);
            }
        }

        [Test]
        public void TestRole2()
        {
            using (var dao = new RoleDao())
            {
                var r = dao.GetRoles();
                int n = Enumerable.Count(r);
                Assert.AreEqual(10, n);
            }

            IQueryable<Role> r2;
            using (var dao = new RoleDao())
            {
                r2 = dao.GetRoles();
                int n = Enumerable.Count(r2);
                Assert.AreEqual(10, n);
            }

            int n2 = Enumerable.Count(r2);
            Assert.AreEqual(10, n2);
        }

        [TearDown]
        public void Clear()
        {

        }

        [TestFixtureTearDown]
        public void ClearAll()
        {
            //一次完整的访问结束后关闭“本次”的 NHibernate Secssion。
            SessionProvider.CloseCurrentSession();
        }
    }
}
