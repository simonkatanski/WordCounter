using System;
using System.Configuration;
using System.IO;
using WordCounter.Data;
using WordCounter.Services;
using WordCounter.Shared.Interfaces;

namespace WordCounter
{
    public class Program
    {
        static void Main(string[] args)
        {
            IAssemblyTypeLoader assemblyTypeLoader = new AssemblyTypeLoader();
            IAppConfiguration configuration = new AppConfiguration();

            try
            {                
                var inputDataProvider = assemblyTypeLoader.LoadType<IInputDataProvider>(configuration.ProviderFactoryAssemblyPath);
                var dataPersistor = assemblyTypeLoader.LoadType<IOutputDataPersistor>(configuration.ProviderFactoryAssemblyPath);
                LaunchSentenceProcessing(inputDataProvider, dataPersistor);
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine("An error occurred during the configuration load. Please check AppConfig is correctly set up. Error details: {0}", ex.Message);
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine("An error occurred. Error details: {0}", ex.Message);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("An error occurred. Provided path is incorrect. Error details: {0}", ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred during the assembly load. Please check if, dependencies are correctly set up. Error details: {0}", ex.Message);
            }
            catch (TypeLoadException ex)
            {
                Console.WriteLine("An error occurred during the assembly load. Error details: {0}", ex.Message);
            }

            Console.WriteLine("Application has finished running and will be closed after key press.");
            Console.ReadKey();
        }

        static void LaunchSentenceProcessing(IInputDataProvider inputDataProvider, IOutputDataPersistor dataPersistor)
        {
            using (var reader = inputDataProvider.GetInputDataReader())
            {
                ISentenceProcessor sentenceProcessor = new SentenceProcessor(new char[] { ' ', ',', '.', ';', ':', '\t' });
                var dictionary = sentenceProcessor.Process(reader);
                dataPersistor.PersistData(dictionary);
            }
        }
    }
}
