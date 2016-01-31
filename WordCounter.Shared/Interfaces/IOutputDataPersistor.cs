using WordCounter.Data;

namespace WordCounter.Shared.Interfaces
{
    /// <summary>
    /// Persists the word counting dictionary into a given output.
    /// </summary>
    public interface IOutputDataPersistor
    {
        void PersistData(WordCountingDictionary inputData);
    }
}
