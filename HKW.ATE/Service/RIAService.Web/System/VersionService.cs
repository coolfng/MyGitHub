using System;
using System.IO;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using HKW.ATE.RIAService.Web.Models.SYS;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HKW.ATE.RIAService.Web.System
{
    [EnableClientAccess()]
    public class VersionService : BaseService
    {
        /// <summary>
        /// 获得版本信息
        /// </summary>
        /// <returns></returns>
        [Query(IsComposable = false)]
        [RequiresAuthentication(ErrorMessage = "登录超时，请重新登录或刷新")]
        public VersionModel GetVersionInfos()
        {
            try
            {
                var versionEntity = new VersionModel { VersionInfo = "", VersionNumber = "" };
                var objReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Version.txt");
                var sLine = "";

                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    versionEntity.VersionNumber = sLine;
                    versionEntity.VersionInfo = sLine+Environment.NewLine;
                }
                versionEntity.VersionInfo += objReader.ReadToEnd();

                objReader.Close();
                return versionEntity;
            }
            catch
            {
                string message = "没有找到版本信息(Version.txt)";
                Logger.Write(message);
                throw new Exception(message);
                //Logger.Write("没有找到版本信息(Version.txt)");
                //return null;
            }
        }

        /// <summary>
        /// 获得版本号
        /// </summary>
        /// <returns></returns>
        [Query(IsComposable = false)]
        [RequiresAuthentication(ErrorMessage = "登录超时，请重新登录或刷新")]
        public VersionModel GetVersionNumber()
        {
            try
            {
                var versionEntity = new VersionModel { VersionInfo = "", VersionNumber = "" };
                var objReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Version.txt");
                var sLine = "";

                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    versionEntity.VersionNumber = sLine;
                    versionEntity.VersionInfo = sLine;
                }

                objReader.Close();
                return versionEntity;
            }
            catch
            {
                string message = "没有找到版本信息(Version.txt)";
                Logger.Write(message);
                throw new Exception(message);
                //Logger.Write("没有找到版本信息(Version.txt)");
                //return null;
            }
        }
    }
}
