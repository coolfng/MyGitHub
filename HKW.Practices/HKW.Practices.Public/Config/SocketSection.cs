using System.Configuration;

namespace HKW.Practices.Public.Config
{
    public sealed class SocketSection : ConfigurationSection
    {
        /// <summary>
        /// 外网 IPaddress
        /// </summary>
        [ConfigurationProperty("internetIPAdress", DefaultValue = "0.0.0.0")]
        public string InternetIPAdress
        {
            get
            {
                return (string)this["internetIPAdress"];
            }
            set
            {
                this["internetIPAdress"] = value;
            }
        }

        /// <summary>
        /// 外网是否启用
        /// </summary>
        [ConfigurationProperty("internetEnable", DefaultValue = "false")]
        public bool InternetEnable
        {
            get
            {
                return (bool)this["internetEnable"];
            }
            set
            {
                this["internetEnable"] = value;
            }
        }

        /// <summary>
        /// 内网 IPaddress
        /// </summary>
        [ConfigurationProperty("intranetIPAdress", DefaultValue = "0.0.0.0")]
        public string IntranetIPAdress
        {
            get
            {
                return (string)this["intranetIPAdress"];
            }
            set
            {
                this["intranetIPAdress"] = value;
            }
        }

        /// <summary>
        /// 内网是否启用
        /// </summary>
        [ConfigurationProperty("intranetEnable", DefaultValue = "false")]
        public bool IntranetEnable
        {
            get
            {
                return (bool)this["intranetEnable"];
            }
            set
            {
                this["intranetEnable"] = value;
            }
        }

        /// <summary>
        /// 与 IPaddress 关联的端口号
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = "11000")]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                this["port"] = value;
            } 
        }

        /// <summary>
        /// 挂起连接队列的最大长度。
        /// </summary>
        [ConfigurationProperty("backlog", DefaultValue = "100")]
        public int BackLog
        {
            get
            {
                return (int)this["backlog"];
            }
            set
            {
                this["backlog"] = value;
            } 
        }

        
    }
}
