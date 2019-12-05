using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying.CommandsExecuting;
using TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying.MyStem;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class MyStemPartOfSpeechQualifierTests
    {
        private MyStemPartOfSpeechQualifier partOfSpeechQualifier;

        [SetUp]
        public void SetUp()
        {
            var pathToMyStemDirectory = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName,
                "TagsCloudContainer", "WordProcessing", "Filtering", "PartsOfSpeechQualifying", "MyStem");
            partOfSpeechQualifier = new MyStemPartOfSpeechQualifier(new CmdCommandExecutor(), new MyStemResultParser(),
                pathToMyStemDirectory);
        }


        [Test]
        public void QualifyPartsOfSpeech_ShouldQualifyAllPartsOfSpeechInRussianLanguage()
        {
            var words = new[]
            {
                "быстрый",
                "быстро",
                "и",
                "ах",
                "пятьдесят",
                "не",
                "из",
                "мы",
                "быстрота",
                "ускориться"
            };

            var result = partOfSpeechQualifier.QualifyPartsOfSpeech(words);
            var expectedPartsOfSpeech = Enum.GetValues(typeof(PartOfSpeech)).Cast<PartOfSpeech>()
                .Where(p => p != PartOfSpeech.Compound);

            result.Select(p => p.Item2).Should().BeEquivalentTo(expectedPartsOfSpeech);
        }
    }
}