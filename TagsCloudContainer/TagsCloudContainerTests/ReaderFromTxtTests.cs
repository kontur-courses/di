using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Reader;

namespace TagsCloudContainerTests
{
    public class ReaderFromTxtTests
    {
        [Test]
        public void GetWordsSetReturnLineFile()
        {
            var pathTempFile = Path.GetTempFileName();
            try
            {
                using (var sw = File.CreateText(pathTempFile))
                    sw.WriteLine("Это пример текстового файла.\nЭто вторая строка текстового файла?");
                var reader = new ReaderLinesFromTxt();
                reader.GetWordsSet(pathTempFile).ToArray().Should()
                    .BeEquivalentTo(new[]
                    {
                        "Это пример текстового файла.",
                        "Это вторая строка текстового файла?"
                    });
            }
            finally
            {
                File.Delete(pathTempFile);
            }
        }
    }
}