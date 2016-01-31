using System.Collections.Generic;

namespace WordCounter.Data
{
    public class WordCountingDictionary : Dictionary<string, int>
    {
        public void Add(string wordToAdd)
        {
            if (ContainsKey(wordToAdd))
            {
                this[wordToAdd]++;
            }
            else
            {
                base.Add(wordToAdd, 1);
            }
        }
    }
}
