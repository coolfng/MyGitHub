using Microsoft.Practices.EnterpriseLibrary.Logging;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace HKW.Practices.NHBase.DaoFundation
{
    public static class HbmMappingExtensions
    {
        public static void ShowInConsole(this HbmMapping mapping)
        {
            var logWriterFactory = new LogWriterFactory();
            var logWriter = logWriterFactory.Create();
            logWriter.Write(mapping.AsString());
        }
    }
}
