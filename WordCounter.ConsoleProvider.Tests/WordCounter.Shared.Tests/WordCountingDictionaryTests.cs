using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WordCounter.Data;

namespace WordCounter.Tests
{
    [TestFixture]
    public class WordCountingDictionaryTests
    {
        [Test]
        public void WhenDictionaryDoesNotContainKeyThenAddKeyToDictionary()
        {
            var dictionary = new WordCountingDictionary();
            dictionary.Add("Word1");
            dictionary.Add("Word1");
            var keyCount = dictionary.Count();
            dictionary.Add("Word2");
            var newWordKeyCount = dictionary.Count();

            Assert.That(dictionary.Count, Is.EqualTo(2));
            Assert.That(keyCount + 1, Is.EqualTo(newWordKeyCount));
        }

        [Test]
        public void WhenDictionaryContainsKeyThenIncrementKeyCount()
        {
            var dictionary = new WordCountingDictionary();
            dictionary.Add("Word1");
            var wordCount = dictionary["Word1"];
            dictionary.Add("Word1");
            var incrementedWordCount = dictionary["Word1"];

            Assert.That(dictionary.Count, Is.EqualTo(1));
            Assert.That(wordCount + 1, Is.EqualTo(incrementedWordCount));
        }

        [Test]
        public void WhenDictionaryContainsKeyAndAddingLowercaseThenAddNewKey()
        {
            var dictionary = new WordCountingDictionary();
            dictionary.Add("Word1");
            var wordCount = dictionary["Word1"];
            dictionary.Add("word1");
            var wordCountAfterAddingLowercase = dictionary["Word1"];

            Assert.That(dictionary.Count, Is.EqualTo(2));
            Assert.That(wordCount, Is.EqualTo(wordCountAfterAddingLowercase));
        }

        [Test]
        public void WhenDictionaryContainsKeyAndQueryingByLowerCaseKeyThenThrowException()
        {
            var dictionary = new WordCountingDictionary();
            dictionary.Add("Word1");            
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var count = dictionary["word1"];
            });            
        }

        [Test]
        public void WhenAddingEmptyStringThenAddStringToDictionary()
        {
            var dictionary = new WordCountingDictionary();
            dictionary.Add(string.Empty);                        
            
            Assert.That(dictionary.Count, Is.EqualTo(1));
            Assert.That(dictionary[string.Empty], Is.EqualTo(1));
        }
    }
}
