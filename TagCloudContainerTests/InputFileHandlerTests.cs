using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.UI;

namespace TagCloudContainerTests
{
    public class InputFileHandlerTests
    {
        private string path;
        private FileStream fs;
        private string[] words;

        [SetUp]
        public void SetUp()
        {
            path = "../testfile.txt";
            words = new TxtReader().FileToWordsArray(path);
            fs = new FileStream(path, FileMode.Create);
            fs.Dispose();
        }


        [Test]
        public void FormFrequencyDictionary_ShouldСorrectFormFrequencyDictionary()
        {
            fs = File.OpenWrite(path);
            var data = "mary\nbloody\nmary\njune";
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            fs.Write(bytes, 0, bytes.Length);
            fs.Dispose();

            var result = InputFileHandler.FormFrequencyDictionary(words, new ConsoleUiSettings());
            result.Should().BeEquivalentTo(new Dictionary<string, int> {{"mary", 2}, {"bloody", 1}, {"june", 1}});
        }
    }
}