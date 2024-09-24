using System.Configuration;

namespace UIAutomation.Config
{
    public class AppConfig
    {
        public string BaseURL { get; }
        public string Browser { get; }

        public AppConfig()
        {
            BaseURL = ConfigurationManager.AppSettings["URL"];
            Browser = ConfigurationManager.AppSettings["Browser"];
        }
    }
}