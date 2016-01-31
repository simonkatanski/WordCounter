namespace WordCounter.Data
{
    public interface IAppConfiguration
    {
        string ProviderFactoryAssemblyPath { get; }
        string PersistorFactoryAssemblyPath { get; }                
    }    
}
