namespace BLLFuncTest
{
    using System.Configuration;

    public class Utility
    {
        public static string GetAppConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}