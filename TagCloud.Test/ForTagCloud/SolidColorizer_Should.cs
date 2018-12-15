using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Models;
using TagCloud.Visualizer.Settings.Colorizer;

namespace TagCloud.Tests.ForTagCloud
{
    [TestFixture]
    class SolidColorizer_Should : TestBase
    {
        [TestCase(1)]
        [TestCase(100)]
        public void ReturnSameColor(int repeats)
        {
            var colorizer = container.Resolve<SolidColorizer>();
            var cloudItem = new CloudItem("", new Rectangle(1, 1, 1, 1), SystemFonts.DefaultFont);
            var brushesList = new List<SolidBrush>();

            for (var i = 0; i < repeats; i++)
                brushesList.Add(colorizer.GetBrush(cloudItem));

            brushesList.Should().OnlyContain(brush => brush.Color == brushesList.First().Color);
        }

    }
}