using System;
using System.Text;
using WordCounter.Data;
using WordCounter.Shared.Interfaces;

namespace WordCounter.Services
{
    /// <summary>
    /// Persists word counting dictionary to console.
    /// </summary>
    public class ConsoleOutputDataPersistor : IOutputDataPersistor
    {      
        public void PersistData(WordCountingDictionary inputData)
        {            
            if (inputData == null)
            {
                throw new ArgumentNullException("inputData");
            }

            Console.WriteLine(ConsoleOutputDataPersistorConsts.ReportHeader);

            var sb = new StringBuilder();
            foreach (var pair in inputData)
            {
                sb.AppendLine(string.Format(ConsoleOutputDataPersistorConsts.LineDisplayPattern, pair.Key, pair.Value));
            }                        

            Console.Write(sb.ToString());
        }

        private static class ConsoleOutputDataPersistorConsts
        {
            public const string ReportHeader = "Word count report:";
            public const string LineDisplayPattern = "{0}: {1}";
        }
    }
}
