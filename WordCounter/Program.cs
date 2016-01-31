using System;
using WordCounter.Data;
using WordCounter.Services;
using WordCounter.Shared.Interfaces;

namespace WordCounter
{
    public class Program
    {
        static void Main(string[] args)
        {
            IAppConfiguration configuration = new AppConfiguration();
            IAssemblyTypeLoader assemblyTypeLoader = new AssemblyTypeLoader();
            var inputDataProvider = assemblyTypeLoader.LoadType<IInputDataProvider>(configuration.ProviderFactoryAssemblyPath);
            var dataPersistor = assemblyTypeLoader.LoadType<IOutputDataPersistor>(configuration.ProviderFactoryAssemblyPath);

            using (var reader = inputDataProvider.GetInputDataReader())
            {
                ISentenceProcessor sentenceProcessor = new SentenceProcessor(new char[] { ' ', ',', '.', ';', ':', '\t' });
                var dictionary = sentenceProcessor.Process(reader);
                dataPersistor.PersistData(dictionary);
            }

            Console.ReadKey();
        }
    }
}
