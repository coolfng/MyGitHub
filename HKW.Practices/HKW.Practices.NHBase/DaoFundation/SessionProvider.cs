using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using Configuration = NHibernate.Cfg.Configuration;

namespace HKW.Practices.NHBase.DaoFundation
{
    public class SessionProvider
    {
        private readonly ISessionStorage _sessionStorage;
        private readonly string _assembly;
        private HbmMapping _hbmMapping;
        private Configuration _config;
        private ISessionFactory _sessionFactory;
        private static SessionProvider _sessionProvider;

        public SessionProvider()
        {
            _sessionStorage = SessionStorageFactory.GetSessionStorage();
            _assembly = Config.DomainAssembly;
        }

        public Configuration NhConfiguration
        {
            get
            {
                if (_config == null)
                {
                    _config = new Configuration();
                    _config.Configure();
                    //if (appConfig.HbmMapping == Mapping.Fluently)
                    //{
                    _config.AddDeserializedMapping(HbmMapping, "");
                    //}
                    //else
                    //{
                    //    foreach (var assembly in GetAssemblyNames())
                    //    {
                    //        _config.AddAssembly(assembly);
                    //    }
                    //}
                    _config = PathConfig(_config);
                }
                return _config;
            }
        }

        private Configuration PathConfig(Configuration config)
        {
            if (File.Exists(Config.LocalConnectionStringPath))
            {
                using (StreamReader sr = new StreamReader(Config.LocalConnectionStringPath))
                {
                    string connection = sr.ReadToEnd();
                    if (config.Properties.ContainsKey(NHibernate.Cfg.Environment.ConnectionString))
                    {
                        config.Properties.Remove(NHibernate.Cfg.Environment.ConnectionString);
                    }

                    var properties = new Dictionary<string, string>
                                  {
                                      {
                                          NHibernate.Cfg.Environment.ConnectionString,
                                          connection
                                      }
                                  };
                    config.AddProperties(properties);
                }
            }
            return config;
        }

        public HbmMapping HbmMapping
        {
            get
            {
                if (_hbmMapping == null)
                {
                    var mapper = new ModelMapper(new EntityModelInspector());
                    foreach (var assembly in GetAssemblyNames())
                    {
                        var t = Assembly.Load(assembly).GetTypes();
                        mapper.AddMappings(t);
                    }
                    _hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
                }
                return _hbmMapping;
            }
        }

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = NhConfiguration.BuildSessionFactory()); }
        }

        private IEnumerable<string> GetAssemblyNames()
        {
            return _assembly.Split(';').ToList();
        }

        public ISession CreateSession()
        {
            if (Config.UserSessionSource)
            {
                ISession s = _sessionStorage.Get();
                if (s == null)
                {
                    s = SessionFactory.OpenSession();
                    _sessionStorage.Set(s);
                }
                return s;
            }
            else
            {
                return SessionFactory.OpenSession();
            }
        }

        public void SaveSession(ISession session)
        {
            _sessionStorage.Set(session);
        }

        public static ISession GetCurrentSession()
        {
            if (_sessionProvider == null)
            {
                _sessionProvider = new SessionProvider();
            }
            return _sessionProvider.CreateSession();
        }

        public static void CloseCurrentSession()
        {
            if (_sessionProvider != null)
            {
                _sessionProvider.SaveSession(null);
            }
        }
    }
}
