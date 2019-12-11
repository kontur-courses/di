using System.Collections.Generic;
using NUnit.Framework;

namespace TagsCloudGeneratorTests.WordsNormalizerTests
{
    public class WordsNormalizerTestsData
    {
        public static IEnumerable<TestCaseData> TestCases => GetTestCases();

        private static IEnumerable<TestCaseData> GetTestCases()
        {
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"раз", "два"},
                        new List<string> {"раз", "два"}
                    })
                .SetName("Words already normalized");
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"!раз.", "!два."},
                        new List<string> {"раз", "два"}
                    })
                .SetName("Words with punctiation");
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"!.", "два"},
                        new List<string> {"два"}
                    })
                .SetName("No empty words");
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"превед"},
                        new List<string>()
                    })
                .SetName("No invalid words");
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"СЛОВО"},
                        new List<string> {"слово"}
                    })
                .SetName("Word in upper case");
        }
    }
}