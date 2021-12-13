using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualizationDI;
using TagsCloudVisualizationDI.Layouter.Filler;

namespace TagsCloudVisualizationDITests
{
    [TestFixture]
    public class FillerTests
    {
        [Test]
        public void ShouldFormElementsCorrectly()
        {
            var elementSize = new Size(100, 200);
            var filler = new CircularCloudLayouterForRectanglesWithText(new Point(0, 0));
            var stringInput = new List<string>
            {
                "карл",
                "карл",
                "красть",
                "красть",
                "красть",
                "коралл"
            };
            var normalyzedWords = new List<Word>();
            foreach (var strWord in stringInput)
            {
                var word = new Word(strWord);
                normalyzedWords.Add(word);
            }
            var formedElements = filler.FormStatisticElements(elementSize, normalyzedWords);

            var expectedResults = new Dictionary<string, int>()
            {
                ["карл"] = 3,
                ["красть"] = 2,
                ["коралл"] = 1,
            };

            formedElements.Keys.Should().BeEquivalentTo(expectedResults.Keys);
            formedElements.Values.Select(v => v.WordElement.CntOfWords).Should().BeEquivalentTo(expectedResults.Values);
        }
    }
}
