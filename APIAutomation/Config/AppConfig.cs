using System.Configuration;

namespace APIAutomation.Config
{
    public class AppConfig
    {
        public string FileLocationPosts { get; }
        public string FileLocationComments { get; }
        public string BaseURI { get; }


        public AppConfig()
        {
            FileLocationPosts = ConfigurationManager.AppSettings["TestData_Posts"];
            FileLocationComments = ConfigurationManager.AppSettings["TestData_Comments"];
            BaseURI = ConfigurationManager.AppSettings["URI"];
        }
    }
}