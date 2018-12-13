using System;
using NUnit.Framework;
using FluentAssertions;
using ConsoleTagClouder;
using TagCloud.Layouters;

namespace Tests
{
    [TestFixture]
    public class GuiParserShould_Should
    {
        [TestCase("CircularCloudLayouter", typeof(CircularCloudLayouter))]
        [TestCase("CircularCloud", typeof(CircularCloudLayouter))]
        [TestCase("Circular", typeof(CircularCloudLayouter))]
        [TestCase("Cir", typeof(CircularCloudLayouter))]
        [TestCase("RowiseCloudLayouter", typeof(RowiseCloudLayouter))]
        [TestCase("RowiseCloud", typeof(RowiseCloudLayouter))]
        [TestCase("Rowise", typeof(RowiseCloudLayouter))]
        [TestCase("Row", typeof(RowiseCloudLayouter))]
        public void ParseLayouterTypeFromOptionValue(string optionValue, Type resultType)
        {
            var settings = Program.ParseSettings(new[] {"\\.","\\.","-l","optionValue"});
            settings.BuildClouderSettings().TLayouter.Should().Be(resultType);
        }

        public void IgnoreIncorrectOption()
        {
            //TODO test console output for warnings
            var settings = Program.ParseSettings(new[] {"\\.","\\.","-q","optionValue"});
            settings.BuildClouderSettings().TLayouter.Should().Be(typeof(RowiseCloudLayouter));
        }
    }
}    