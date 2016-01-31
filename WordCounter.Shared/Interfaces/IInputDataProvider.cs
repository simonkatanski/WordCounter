using System.IO;

namespace WordCounter.Shared.Interfaces
{
    /// <summary>
    /// Gets the input data reader for further processing.
    /// </summary>
    public interface IInputDataProvider
    {
        TextReader GetInputDataReader();
    }
}
