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
            var container = AutofacConfig.ConfigureContainer(args);

            container.Resolve<TagCloud>().Tags.Length.Should().Be(63);
        }

        [Test]
        public void Constructor_CorrectExcludeBoringWords()
        {
            var args = new string[] { "-i", baseDomain + "input/input.txt", "-o", baseDomain + "output/o.png", "--words-to-exclude", baseDomain + "input/words to exclude.txt" };

            var container = AutofacConfig.ConfigureContainer(args);

            var wordsToExclude = container.ResolveNamed<HashSet<string>>("WordsToExclude");
            container
                .Resolve<WordPreprocessing>()
                .ToLower()
                .Exclude(wordsToExclude)
                .IgnoreInvalidWords();

            container.Resolve<TagCloud>().Tags.Length.Should().Be(51);
        }

        [Test]
        public void Constructor_OnlyInput()
        {
            var args = new string[] { "-i", baseDomain + "input/input.txt" };
            var container = AutofacConfig.ConfigureContainer(args);

            Action ctorInvocation = () => container.Resolve<TagCloud>();
            ctorInvocation.Should().Throw<DependencyResolutionException>();
        }

        [Test]
        public void Constructor_OnlyOutput()
        {
            var args = new string[] { "-o", baseDomain + "output/o.png" };
            var container = AutofacConfig.ConfigureContainer(args);

            Action ctorInvocation = () => container.Resolve<TagCloud>();
            ctorInvocation.Should().Throw<DependencyResolutionException>();
        }

        [Test]
        public void Constructor_OnlyColor()
        {
            var args = new string[] { "-c", "red" };
            var container = AutofacConfig.ConfigureContainer(args);

            Action ctorInvocation = () => container.Resolve<TagCloud>();
            ctorInvocation.Should().Throw<DependencyResolutionException>();
        }

        [Test]
        public void Constructor_Correct()
        {
            var args = new string[] { "-i", baseDomain + "input/input.txt", "-o", baseDomain + "output/o.png", "-c", "red",
                "--words-to-exclude", baseDomain + "input/words to exclude.txt", "-f", "Arial" };

            var container = AutofacConfig.ConfigureContainer(args);
            
            var brush = container.Resolve<Brush>();
            var fontName = container.ResolveNamed<string>("FontName");
            var outputFilePath = container.Resolve<ArgumentsParser>().OutputPath;
            
            brush.Should().Be(Brushes.Red);
            fontName.Should().Be("Arial");
            outputFilePath.Should().Be(baseDomain + "output/o.png");

        }
    }

}
