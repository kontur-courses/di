using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using NUnit.Framework;
using TagsCloudVisualizationDI;
using TagsCloudVisualizationDI.FileReader;
using TagsCloudVisualizationDI.Layouter.Filler;

namespace TagsCloudVisualizationDITests
{
    [TestFixture]
    public class FileReaderTests
    {
        [Test]
        public void ShouldNotThrowExceptionOnCoerrectPath()
        {
            var path = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\ex1.TXT";
            var savePath = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\result.TXT";
            var fileReader = new DefaultTextFileReader(savePath, Encoding.UTF8);
            Action read = () => fileReader.ReadText();
            read.Should().NotThrow();
        }

        [Test]
        public void ShouldThrowExceptionIfPathNotCorrect()
        {
            var path = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "abraccadabras";
            var savePath = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\result.TXT";
            var fileReader = new DefaultTextFileReader(savePath, Encoding.UTF8);
            Action read = () => fileReader.ReadText();
            read.Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void ShouldBeCorrectCntOfLines()
        {
            var path = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\ex1.TXT";
            var savePath = Path.GetDirectoryName(typeof(Program).Assembly.Location) + "\\result.TXT";
            var fileReader = new DefaultTextFileReader(savePath, Encoding.UTF8);
            var lines =  fileReader.ReadText();
            lines.Length.Should().Be(8);
        }
    }
}
