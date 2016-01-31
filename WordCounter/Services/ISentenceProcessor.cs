using System.IO;
using WordCounter.Data;

namespace WordCounter.Services
{
    /// <summary>
    /// Processes input from TextReader based class and creates a dictionary with information on word occurrences.
    /// </summary>
    public interface ISentenceProcessor
    {
        WordCountingDictionary Process(TextReader inputReader);
    }
}