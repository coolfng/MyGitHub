using System.Configuration;

namespace HKW.Practices.Public.Config
{
    public class ConfigManager
    {
        private static Configuration _config;
        public static T GetSection<T>() where T : ConfigurationSection, new()
        {
            T section;
            if (_config==null)
            {
                _config = ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);
                //var exePath = System.Web.HttpContext.Current.Server.MapPath("/");
                //_config = ConfigurationManager.OpenExeConfiguration(exePath);
                //_config = WebConfigurationManager.OpenWebConfiguration(exePath);
            }
            
            if (_config.Sections[typeof(T).Name] == null)
            {
                section = new T();
                _config.Sections.Add(typeof(T).Name, section);
                section.SectionInformation.ForceSave = true;
                _config.Save(ConfigurationSaveMode.Full);
            }
            else
            {
                section = _config.GetSection(typeof(T).Name) as T;
            }
            return section;
        }
    }
}
