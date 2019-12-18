using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using FluentAssertions;
using NUnit.Framework;
using TextPreprocessor.TextRiders;

namespace TextPreprocessor.Tests
{
    [TestFixture]
    public class TextRiderTests
    {
        private const string TestData = "Every time we lie awake\n" + 
                                        "After every hit we take\n" +
                                        "Every feeling that I get\n" +
                                        "But I haven't missed you yet\n";

        private const string SkipWords = "we i but you";

        private IFileTextRider fileTextRider;
            
        [SetUp]
        public void SetUp()
        {
            using (var sw = new StreamWriter("test.txt"))
                sw.Write(TestData);
            using (var sw = new StreamWriter("SkipWords.txt"))
                sw.Write(SkipWords);
            
            fileTextRider = new TxtTextRider(TextRiderConfig.Default());
            fileTextRider.RiderConfig.FilePath = "test.txt";
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete("test.txt");
            File.Delete("SkipWords.txt");
        }

        [Test]
        public void AllWordsMustMeLowercase()
        {
            var tags = fileTextRider.GetTags().ToArray();

            foreach (var tag in tags)
            {
                tag.Content
                    .Should()
                    .BeEquivalentTo(tag.Content.ToLower());
            }
        }

        [Test]
        public void SkippingWordsShouldBeExcluded()
        {
            var tags = fileTextRider.GetTags().ToArray();
            var skippingWords = SkipWords.Split().ToArray();

            foreach (var skippingWord in skippingWords)
            {
                tags.Should().NotContain(skippingWord);
            }
        }
    }
}