using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTest
{
    [TestFixture]
    class StringExtensionTest
    {
        [TestCase("hello.txt", ExpectedResult = "txt", TestName = "extract valid extension")]
        [TestCase("hello", ExpectedResult = null, TestName = "return null when no extension")]
        [TestCase("hello.", ExpectedResult = "", TestName = "return empty string when empty extension")]
        [TestCase("hello.txt.exe", ExpectedResult = "exe", TestName = "return last extension when has several")]
        public string ExtractFileExtension_Should(string fileName)
        {
            return fileName.ExtractFileExtension();
        }

        [TestCase("abc abc", 0, ExpectedResult = 3, 
            TestName = "return first position when predicate is true")]
        [TestCase("abc", 0, ExpectedResult = 3, 
            TestName = "return last position in string when predicate always false")]
        public int SkipUntil_Should(string text, int startPos)
        {
            return text.SkipUntil(startPos, ch => ch == ' ');
        }

    }
}
