using System.Collections.Generic;
using NUnit.Framework;

namespace TagsCloudGeneratorTests.WordsFilterTests
{
    public class WordsFilterTestsData
    {
        public static IEnumerable<TestCaseData> TestCases => GetTestCases();

        private static IEnumerable<TestCaseData> GetTestCases()
        {
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"на", "по", "вследствие"},
                        new List<string>()
                    })
                .SetName("No prepositions");
            
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"быстро", "далеко"},
                        new List<string>()
                    })
                .SetName("No adverbs");
            
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"и", "или", "да"},
                        new List<string>()
                    })
                .SetName("No conjunctions");
            
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"неужели", "разве", "ли"},
                        new List<string>()
                    })
                .SetName("No particles");
            
            yield return new TestCaseData(
                    new object[]
                    {
                        new List<string> {"ты", "я", "он"},
                        new List<string>()
                    })
                .SetName("No pronouns");
        }
    }
}