using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WordCounter.Services
{
    internal class AssemblyTypeLoader : IAssemblyTypeLoader
    {
        public T LoadType<T>(string assemblyFileName)
        {
            var assembly = LoadAssembly(assemblyFileName);

            var typeToFind = typeof(T);
            var typeFound = assembly.GetTypes().FirstOrDefault(typeFromAssembly => typeToFind.IsAssignableFrom(typeFromAssembly));
            if(typeFound != null)
            {
                return (T)Activator.CreateInstance(typeFound);
            }
            throw new TypeLoadException(string.Format("Assembly {0}, does not contain implementation of type: {1}", assemblyFileName, typeFound));
        }

        private Assembly LoadAssembly(string assemblyFileName)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dllPath = Path.Combine(path, assemblyFileName);
            return Assembly.LoadFile(dllPath);
        }
    }
}