using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.FileOpeners;

namespace TagCloudContainerTests
{
    public class InputFileHandlerTests
    {
        private string path;
        private FileStream fs;

        [SetUp]
        public void SetUp()
        {
            path = "../testfile2.txt";
            fs = new FileStream(path, FileMode.Create);
            fs.Dispose();
        }

        [Test]
        public void FormFrequencyDictionary_ShouldThrowArgumentException_OnEmptyFile()
        {
            var handler = new TagsCloudContainer.InputFileHandler(new TxtReader());
            Action act = () => handler.FormFrequencyDictionary(path);
            act.Should().Throw<ArgumentException>().WithMessage("Empty file");
        }

        [Test]
        public void FormFrequencyDictionary_ShouldСorrectFormFrequencyDictionary()
        {
            fs = File.OpenWrite(path);
            var data = "mary\nbloody\nmary\njune";
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            fs.Write(bytes, 0, bytes.Length);
            fs.Dispose();

            var handler = new TagsCloudContainer.InputFileHandler(new TxtReader());
            var result = handler.FormFrequencyDictionary(path);
            result.Should().BeEquivalentTo(new Dictionary<string, int> {{"mary", 2}, {"bloody", 1}, {"june", 1}});
        }
    }
}