using NUnit.Framework;
using FluentAssertions;
using TagsCloudContainer;
using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudContainerTest
{
    public class LineByLineReaderTest
    {
        [Test]
        public void CheckLinesCountInInputFile()
        {
            var pathToTextFile = @"..\..\..\Files\LineByLineText.txt";
            var reader = new LineByLineWordReader();
            var words = reader.Read(pathToTextFile);

            foreach (var w in words)
                Console.WriteLine(w);

            words.Count().Should().Be(7);
        }
    }
}
