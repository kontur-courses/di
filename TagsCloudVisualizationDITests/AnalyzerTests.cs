using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TagsCloudVisualizationDI;
using TagsCloudVisualizationDI.Settings;

namespace TagsCloudVisualizationDITests
{
    [TestFixture]
    public class AnalyzerTests
    {
        [Test]
        public void ShouldNotThrowWhenPathsAreValid()
        {
            var settings = new DefaultSettingsConfiguration(string.Empty, string.Empty, ImageFormat.Png, null);
            var analyzer = ((ISettingsConfiguration)settings).Analyzer;
            Action invoking = () => analyzer.InvokeMystemAnalization();
            invoking.Should().NotThrow();

        }

        [Test]
        public void ShouldGetRightAnalyzedWords()
        {
            var path = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\ex2.TXT";
            var savePath = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\result.TXT";

            var settings = (ISettingsConfiguration)new DefaultSettingsConfiguration(path, savePath, ImageFormat.Png, null);
            var analyzer = settings.Analyzer;
            var reader = settings.FileReader;
            analyzer.InvokeMystemAnalization();
            var words = reader.ReadText();
            var result = analyzer.GetAnalyzedWords(words).ToList();
            var expectedResult = new List<Word>
            {
                new Word("тот"),
                new Word("ураган"),
                new Word("проходить"),
                new Word("мы"),
                new Word("мало"),
                new Word("уцелеть"),
            };
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
