using System.Configuration;

namespace CyberByte.ArmaAdmin.Launcher
{
    class Util
    {
        public static string FetchConfigValue(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}
