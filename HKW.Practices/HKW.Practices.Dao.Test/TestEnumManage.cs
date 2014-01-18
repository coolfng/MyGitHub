using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HKW.Practices.Domain.Dao;
using HKW.Practices.Domain.Entities.SYS;
using HKW.Practices.NHBase.DaoFundation;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace HKW.Practices.Dao.Test
{
    [TestFixture]
    public class TestEnumManage
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
        public void TestEnumHeader()
        {
            using (var dao = new BaseDao())
            {
                var r = new EnumHeader { HeaderGroup = "AAA", Code = "001", Name = "A01" };
                dao.Save(r);
                Assert.IsFalse(r.DelFlag);
            } 
            
            using (var dao = new BaseDao())
            {
                var r = new EnumHeader { HeaderGroup = "AAA", Code = "002", Name = "A02" };
                dao.Save(r);
                Assert.IsFalse(r.DelFlag);
            }

            using (var dao = new BaseDao())
            {
                var r = new EnumHeader { HeaderGroup = "AAA", Code = "003", Name = "A03" };
                dao.Save(r);
                Assert.IsFalse(r.DelFlag);
            }

            using (var dao = new BaseDao())
            {
                var r = new EnumHeader { HeaderGroup = "AAA", Code = "004", Name = "A04" };
                dao.Save(r);
                Assert.IsFalse(r.DelFlag);
            }

            using (var dao = new BaseDao())
            {
                var r = new EnumHeader { HeaderGroup = "BBB", Code = "B001", Name = "B01" };
                dao.Save(r);
                Assert.IsFalse(r.DelFlag);
            }

            using (var dao = new BaseDao())
            {
                var r = new EnumHeader { HeaderGroup = "BBB", Code = "B002", Name = "B02" };
                dao.Save(r);
                Assert.IsFalse(r.DelFlag);
            }

            using (var dao = new BaseDao())
            {
                var r = new EnumHeader { HeaderGroup = "BBB", Code = "B003", Name = "B03" };
                dao.Save(r);
                Assert.IsFalse(r.DelFlag);
            }
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
