using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.UI;

namespace TagCloudContainerTests
{
    public class InputFileHandlerTests
    {
        private string[] words;
        private IUi settings;

        [SetUp]
        public void SetUp()
        {
            settings = Parser.Default.ParseArguments<ConsoleUiSettings>(new string[] { }).Value;
        }


        [Test]
        public void FormFrequencyDictionary_ShouldСorrectFormFrequencyDictionary()
        {
            words = new[] {"mary", "BlooDy", "MaRy", "june"};
            var result = InputFileHandler.FormFrequencyDictionary(words, settings);
            result.Should().BeEquivalentTo(new Dictionary<string, int> {{"mary", 2}, {"bloody", 1}, {"june", 1}});
        }
    }
}