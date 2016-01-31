using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using WordCounter.Services;

namespace WordCounter.Tests.WordCounter.Tests
{
    [TestFixture]
    public class SentenceProcessorTests
    {
        readonly char[] _standardDelimiters = { ' ', ',', '.', ';', ':', '\t' };

        [Test]
        public void WhenCreatingSentenceProcessorAndNullDelimitersProvidedThenThrowNullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => new SentenceProcessor(null));
        }

        [Test]
        public void WhenCreatingSentenceProcessorAndNullReaderProvidedThenThrowNullArgumentException()
        {
            var sentenceProcessor = new SentenceProcessor(new List<char>());
            Assert.Throws<ArgumentNullException>(() => sentenceProcessor.Process(null));
        }

        [Test]
        public void WhenInputPassedThenCountWordOccurrences()
        {
            var mockedTextReader = new Mock<TextReader>();
            mockedTextReader.SetupSequence(p => p.ReadLine())
                .Returns(GetFirstTestLine())
                .Returns(GetSecondTestLine());

            ISentenceProcessor sentenceProvider = new SentenceProcessor(_standardDelimiters);

            var result = sentenceProvider.Process(mockedTextReader.Object);
            Assert.That(result["line"], Is.EqualTo(4));
            Assert.That(result["test"], Is.EqualTo(2));
            Assert.That(result["first"], Is.EqualTo(1));
            Assert.That(result["second"], Is.EqualTo(1));
        }

        [Test]
        public void WhenInputPassedThenCountWithoutCaseSensitivity()
        {
            var mockedTextReader = new Mock<TextReader>();
            mockedTextReader.SetupSequence(p => p.ReadLine())
                .Returns("test")
                .Returns("Test");

            ISentenceProcessor sentenceProvider = new SentenceProcessor(_standardDelimiters);
            var result = sentenceProvider.Process(mockedTextReader.Object);

            Assert.That(result["test"], Is.EqualTo(2));            
        }

        [Test]
        public void WhenTextReaderReturnsNullThenCreateEmptyDictionary()
        {
            var mockedTextReader = new Mock<TextReader>();
            mockedTextReader.SetupSequence(p => p.ReadLine())
                .Returns(null);                

            ISentenceProcessor sentenceProvider = new SentenceProcessor(_standardDelimiters);
            var result = sentenceProvider.Process(mockedTextReader.Object);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void WhenTextReaderReturnsEmptyStringThenCreateEmptyDictionary()
        {
            var mockedTextReader = new Mock<TextReader>();
            mockedTextReader.SetupSequence(p => p.ReadLine())
                .Returns("");

            ISentenceProcessor sentenceProvider = new SentenceProcessor(_standardDelimiters);
            var result = sentenceProvider.Process(mockedTextReader.Object);

            Assert.That(result.Count, Is.EqualTo(0));            
        }

        [Test]
        public void WhenNoDelimitersPassedThenOnlySplitBySpace()
        {
            var testLine = "line line.line";

            var mockedTextReader = new Mock<TextReader>();
            mockedTextReader.SetupSequence(p => p.ReadLine())
                .Returns(testLine);

            ISentenceProcessor sentenceProvider = new SentenceProcessor(new List<char>());
            var result = sentenceProvider.Process(mockedTextReader.Object);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result["line"], Is.EqualTo(1));
            Assert.That(result["line.line"], Is.EqualTo(1));
        }

        private string GetFirstTestLine()
        {
            return "First test line line, line.";
        }

        private string GetSecondTestLine()
        {
            return "Second test line.";
        }
    }
}
