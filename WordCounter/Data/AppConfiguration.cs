using System.Configuration;

namespace WordCounter.Data
{
    public class AppConfiguration : IAppConfiguration
    {
        public string PersistorFactoryAssemblyPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ProviderFactoryAssemblyPath"];
            }
        }

        public string ProviderFactoryAssemblyPath
        {
            get
            {
                return ConfigurationManager.AppSettings["PersistorFactoryAssemblyPath"];
            }
        }
    }
}
