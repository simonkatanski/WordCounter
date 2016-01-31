using System;
using System.IO;
using WordCounter.Shared.Interfaces;

namespace WordCounter.ConsoleProvider
{
    /// <summary>
    /// Gets the console's standard input, as an input for further data processing
    /// </summary>
    public class ConsoleInputDataProvider : IInputDataProvider
    {
        private const string InputHeader = "Press Ctrl + Z on an empty line and Enter to complete the word input. \nEnter sentence for processing:";
        
        public TextReader GetInputDataReader()
        {
            Console.WriteLine(InputHeader);            
            return Console.In;
        }
    }
}
