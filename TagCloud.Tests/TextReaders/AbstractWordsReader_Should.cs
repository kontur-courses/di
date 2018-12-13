using NUnit.Framework;
using TagCloud.Core.WordsParsing.WordsReading;

namespace TagCloud.Tests.TextReaders
{
    public class AbstractWordsReader_Should<TReader> where TReader : IWordsReader
    {
        protected string baseDir = TestContext.CurrentContext.TestDirectory + @"\..\..\Resources\";
        protected TReader reader;
    }
}