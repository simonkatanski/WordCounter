using System;
using NUnit.Framework;
using WordCounter.Data;
using WordCounter.Services;
using WordCounter.Shared.Interfaces;
using System.IO;

namespace WordCounter.Tests
{
    [TestFixture]
    public class ConsoleOutputDataPersistorTests
    {
        private IOutputDataPersistor _outputDataPersistor;
        private StreamWriter _standardOutputReset;

        [SetUp]
        public void SetUp()
        {
            _outputDataPersistor = new ConsoleOutputDataPersistor();
            _standardOutputReset = new StreamWriter(Console.OpenStandardOutput());
            _standardOutputReset.AutoFlush = true;
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_standardOutputReset);
        }
        
        [Test]
        public void WhenEmptyCountDictionaryPassedThenDisplayHeaderInTheConsole()
        {
            using (StringWriter testWriter = new StringWriter())
            {
                Console.SetOut(testWriter);
                _outputDataPersistor.PersistData(GetEmptyDictionary());
                string expected = string.Format("Word count report:{0}", Environment.NewLine);
                Assert.That(expected, Is.EqualTo(testWriter.ToString()));
            }
        }

        [Test]
        public void WhenNullCountDictionaryPassedThenThrowArgumentNullException()
        {            
            using (StringWriter testWriter = new StringWriter())
            {
                Console.SetOut(testWriter);                
                Assert.Throws<ArgumentNullException>(() => _outputDataPersistor.PersistData(GetNullDictionary()));                
            }
        }

        [Test]        
        public void WhenTwoItemsCountDictionaryPassedThenDisplayItemInTheConsole()
        {
            using (StringWriter testWriter = new StringWriter())
            {
                Console.SetOut(testWriter);
                _outputDataPersistor.PersistData(GetTwoItemDictionary());
                string expected = string.Format("Word count report:{0}{1}: {2}{0}{3}: {2}{0}", Environment.NewLine, "Word1", 1, "Word2");
                Assert.That(testWriter.ToString, Is.EqualTo(expected));
            }
        }

        [Test]
        public void WhenEmptyItemInCountDictionaryPassedThenDisplayEmptyItemInTheConsole()
        {
            using (StringWriter testWriter = new StringWriter())
            {
                Console.SetOut(testWriter);
                _outputDataPersistor.PersistData(GetStrangeDictionary());
                string expected = string.Format("Word count report:{0}{1}: {2}{0}", Environment.NewLine, string.Empty, 1);
                Assert.That(expected, Is.EqualTo(testWriter.ToString()));
            }
        }
        
        private WordCountingDictionary GetEmptyDictionary()
        {
            return new WordCountingDictionary();
        }

        private WordCountingDictionary GetNullDictionary()
        {
            return null;
        }

        private WordCountingDictionary GetTwoItemDictionary()
        {
            var dict = new WordCountingDictionary();
            dict.Add("Word1");
            dict.Add("Word2");
            return dict;
        }

        private WordCountingDictionary GetStrangeDictionary()
        {
            var dict = new WordCountingDictionary();
            dict.Add("");            
            return dict;
        }
    }
}
