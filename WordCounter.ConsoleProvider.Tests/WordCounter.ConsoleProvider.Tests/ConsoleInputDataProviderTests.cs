using NUnit.Framework;
using System;
using System.IO;
using WordCounter.ConsoleProvider;
using WordCounter.Shared.Interfaces;

namespace WordCounter.Tests.WordCounter.ConsoleProvider.Tests
{
    [TestFixture]
    public class ConsoleInputDataProviderTests
    {
        private const string TestString1 = "Test string entered";

        [Test]
        public void WhenInputDataReaderReturnedThenExpectEnteredDataFromStandardInput()
        {
            IInputDataProvider provider = new ConsoleInputDataProvider();
            using (TextReader tr = new StringReader(TestString1))
            {
                Console.SetIn(tr);
                using (var consoleReader = provider.GetInputDataReader())
                {
                    var input = consoleReader.ReadToEnd();
                    Assert.That(input, Is.EqualTo(TestString1));
                }                
            }
        }
    }
}
