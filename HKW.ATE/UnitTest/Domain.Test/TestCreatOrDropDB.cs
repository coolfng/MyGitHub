using HKW.Practices.NHBase.DaoFundation;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace HKW.ATE.Domain.Test
{
    [TestFixture]
    public class TestCreatOrDropDb
    {
        [TestFixtureSetUp]
        public void AllSetUp()
        {
            new Configuration();
        }
        [SetUp]
        public void SetUp()
        {

        }

        /// <summary>
        /// 创建表
        /// </summary>
        [Test]
        public void CreatDB()
        {
            var p = new SessionProvider();
            p.HbmMapping.ShowInConsole();
            var schemaExport = new SchemaExport(p.NhConfiguration);
            schemaExport.Create(true, true);
        }
        /// <summary>
        /// 删除表
        /// </summary>
        [Test]
        public void DropDB()
        {
            var p = new SessionProvider();
            p.HbmMapping.ShowInConsole();
            var schemaExport = new SchemaExport(p.NhConfiguration);
            schemaExport.Drop(true, true);
        }
        [TearDown]
        public void Clear()
        {

        }
        [TestFixtureTearDown]
        public void AllClear()
        {

        }

    }
}
