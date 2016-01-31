namespace WordCounter.Services
{
    /// <summary>
    /// Loads given type from the assembly.
    /// </summary>
    internal interface IAssemblyTypeLoader
    {
        T LoadType<T>(string assemblyFileName);
    }
}