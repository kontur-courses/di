using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class FileAnalyzer_GetWordsFrequency_Should
    {
        //[Test]
        //public void Frequency_SimpleTest()
        //{
        //    var boringWordDetrminer = new BoringWordsDeterminer();
        //    var actual = new FileAnalyzer(boringWordDetrminer, 50, 0).GetWordsFrequensy(new List<string>() {"Hello", "Hello", "!"});
        //    var expected = new Dictionary<string, int>(){{"Hello",2}, {"!", 1}};
        //    actual.ShouldBeEquivalentTo(expected);
        //}

        //[Test]
        //public void Frequency_TestOnBigFile()
        //{
        //    var boringWordDetrminer = new BoringWordsDeterminer();
        //    var actual = new FileAnalyzer(boringWordDetrminer, 3, 5).GetWordsFrequensy(File.ReadLines(TestContext.CurrentContext.TestDirectory+"\\book1.txt"));
        //    var expected = new Dictionary<string, int>(){{"Prince", 237}, {"Pierre", 227}, {"princess", 117}};
        //    actual.ShouldBeEquivalentTo(expected);
        //}
    }
}