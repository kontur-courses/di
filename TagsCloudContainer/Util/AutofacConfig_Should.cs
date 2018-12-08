using NUnit.Framework;
using System;
using System.Collections.Generic;
using Autofac;
using TagsCloudContainer.Cloud;
using FluentAssertions;
using Autofac.Core;
using System.Drawing;
using TagsCloudContainer.Arguments;

namespace TagsCloudContainer.Util
{
    [TestFixture]
    public class AutofacConfig_Should
    {
        private string baseDomain = $"{AppDomain.CurrentDomain.BaseDirectory}/";
        [Test]
        public void Constructor_CorrectInputAndOutput()
        {
            var args = new string[] { "-i", baseDomain + "input/input.txt", "-o", baseDomain + "output/o.png" };
            var container = new AutofacContainer(args);

            container.TagCloud.Tags.Length.Should().Be(55);
        }

        [Test]
        public void Constructor_CorrectExcludeBoringWords()
        {
            var args = new string[] { "-i", baseDomain + "input/input.txt", "-o", baseDomain + "output/o.png", "--words-to-exclude", baseDomain + "input/words to exclude.txt" };

            var container = new AutofacContainer(args);
            
            container.TagCloud.Tags.Length.Should().Be(51);
        }

        [Test]
        public void Constructor_OnlyInput()
        {
            var args = new string[] { "-i", baseDomain + "input/input.txt" };
            var container = new AutofacContainer(args);
            TagCloud tagCloud;
            Action ctorInvocation = () => tagCloud = container.TagCloud;
            ctorInvocation.Should().Throw<DependencyResolutionException>();
        }

        [Test]
        public void Constructor_OnlyOutput()
        {
            var args = new string[] { "-o", baseDomain + "output/o.png" };
            var container = new AutofacContainer(args);

            TagCloud tagCloud;
            Action ctorInvocation = () => tagCloud = container.TagCloud;
            ctorInvocation.Should().Throw<DependencyResolutionException>();
        }

        [Test]
        public void Constructor_OnlyColor()
        {
            var args = new string[] { "-c", "red" };
            var container = new AutofacContainer(args);

            TagCloud tagCloud;
            Action ctorInvocation = () => tagCloud = container.TagCloud;
            ctorInvocation.Should().Throw<DependencyResolutionException>();
        }

        [Test]
        public void Constructor_Correct()
        {
            var args = new string[] { "-i", baseDomain + "input/input.txt", "-o", baseDomain + "output/o.png", "-c", "red",
                "--words-to-exclude", baseDomain + "input/words to exclude.txt", "-f", "Arial" };

            var container = new AutofacContainer(args);            

            container.Brush.Should().Be(Brushes.Red);
            container.FontName.Should().Be("Arial");
            container.OutputPath.Should().Be(baseDomain + "output/o.png");

        }
    }

}
