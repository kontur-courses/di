using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Visualizing.ColorHandling;

namespace TagsCloudContainerTests
{
    public class AlternateColorHandlerTests
    {
        [Test]
        public void BackgroundColor_ShouldReturnDefaultColor_WhenInputColorsIsEmpty()
        {
            var colorHandler = new AlternateColorHandler();

            var backgroundColor = colorHandler.BackgroundColor;

            backgroundColor.Should().NotBeEquivalentTo(Color.Empty);
        }

        [Test]
        public void GetColorFor_ShouldReturnDefaultColor_WhenInputColorsIsEmpty()
        {
            var colorHandler = new AlternateColorHandler();

            var color = colorHandler.GetColorFor(string.Empty, Rectangle.Empty);

            color.Should().NotBeEquivalentTo(Color.Empty);
        }

        [TestCaseSource(nameof(BackgroundColorShouldReturnFirstColorInInputWhenInputColorsContainsAtLeastTwoColorsTestCases))]
        public void BackgroundColor_ShouldReturnFirstColorInInput_WhenInputColorsContainsAtLeastTwoColors(List<Color> colors)
        {
            var colorHandler = new AlternateColorHandler();
            colorHandler.SetColorsToUse(colors);

            var backgroundColor = colorHandler.BackgroundColor;

            backgroundColor.Should().BeEquivalentTo(colors.First());
        }

        private static IEnumerable BackgroundColorShouldReturnFirstColorInInputWhenInputColorsContainsAtLeastTwoColorsTestCases
        {
            get
            {
                yield return new TestCaseData(new List<Color> { Color.AliceBlue, Color.Aqua }).SetName("2 colors");
                yield return new TestCaseData(new List<Color> {Color.AliceBlue, Color.Black, Color.Yellow}).SetName(
                    "3 colors");
                yield return new TestCaseData(new List<Color>
                    {Color.AliceBlue, Color.Black, Color.Yellow, Color.Black, Color.Black}).SetName(
                    "5 colors");
            }
        }

        [Test]
        public void GetColorFor_ShouldReturnColorFromInput_WhenInputColorsContainsOnlyOneColor()
        {
            var colorHandler = new AlternateColorHandler();
            colorHandler.SetColorsToUse(new List<Color> {Color.Azure});

            var color = colorHandler.GetColorFor(string.Empty, Rectangle.Empty);

            color.Should().BeEquivalentTo(Color.Azure);
        }

        [Test]
        public void GetColorFor_ShouldReturnSecondColor_WhenInputContainsTwoColors()
        {
            var colorHandler = new AlternateColorHandler();
            colorHandler.SetColorsToUse(new List<Color> {Color.Azure, Color.Blue});

            var color = colorHandler.GetColorFor(string.Empty, Rectangle.Empty);

            color.Should().BeEquivalentTo(Color.Blue);
        }

        [Test]
        public void GetColorFor_ShouldAlternateColors_WhenInputColorsContainsMoreThanTwoColors()
        {
            var colorHandler = new AlternateColorHandler();
            colorHandler.SetColorsToUse(new List<Color> { Color.Azure, Color.Blue, Color.Aqua });

            var firstColor = colorHandler.GetColorFor(string.Empty, Rectangle.Empty);
            var secondColor = colorHandler.GetColorFor(string.Empty, Rectangle.Empty);
            var thirdColor = colorHandler.GetColorFor(string.Empty, Rectangle.Empty);

            firstColor.Should().BeEquivalentTo(Color.Blue);
            secondColor.Should().BeEquivalentTo(Color.Aqua);
            thirdColor.Should().BeEquivalentTo(Color.Blue);
        }
    }
}