using HKW.Practices.NHBase.DaoFundation;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace HKW.Practices.Dao.Test
{
    [TestFixture]
    public class TestDbSchema
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
        public void TestCreateDb()
        {
            var p = new SessionProvider();
            p.HbmMapping.ShowInConsole();
            var schemaExport = new SchemaExport(p.NhConfiguration);
            schemaExport.Create(false, true);
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
