using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordCounter.Data;

namespace WordCounter.Services
{
    public class SentenceProcessor : ISentenceProcessor
    {
        private readonly char[] _delimiters;        
        private readonly WordCountingDictionary _dictionary = new WordCountingDictionary();

        public SentenceProcessor(IEnumerable<char> delimiters)
        {
            if(delimiters == null)
            {
                throw new ArgumentNullException("delimiters");
            }

            _delimiters = delimiters.ToArray();            
        }

        public WordCountingDictionary Process(TextReader inputReader)
        {
            if (inputReader == null)
            {
                throw new ArgumentNullException("inputReader");
            }

            while (true)
            {
                var line = inputReader.ReadLine();
                if (line == null)
                    break;

                ProcessLine(line);
            }

            return _dictionary;
        }

        protected internal void ProcessLine(string lineToProcess)
        {
            var wordsArray = lineToProcess.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
            foreach(var word in wordsArray)
            {
                _dictionary.Add(word.ToLowerInvariant());
            }
        }
    }
}
