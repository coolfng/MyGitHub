using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Server;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HKW.ATE.RIAService.Web
{
    public class BaseService : DomainService
    {
        protected const string UnloginErrMsg = "登录超时，请重新登录或刷新";

        public BaseService()
        {
            var logWriterFactory = new LogWriterFactory();
            var logWriter = logWriterFactory.Create();
            Logger.SetLogWriter(logWriter);
        }
    }
}
