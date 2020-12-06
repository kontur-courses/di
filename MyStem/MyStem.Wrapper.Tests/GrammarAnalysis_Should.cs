using System.IO;
using FluentAssertions;
using MyStem.Wrapper.Impl;
using NUnit.Framework;

namespace MyStem.Wrapper.Tests
{
    // ReSharper disable once InconsistentNaming
    public class GrammarAnalysis_Should
    {
        private IGrammarAnalyser analyser;

        [SetUp]
        public void SetUp()
        {
            analyser = new GrammarAnalyser(new MyStemBuilder(Path.Combine(TestContext.CurrentContext.WorkDirectory,
                "../../../../bin/", "mystem.exe")));
        }

        [Test]
        public void CreateRawResultModel_ForEachWord()
        {
            PerformTest("барашки должны плодиться")
                .Should()
                .HaveCount(3);
        }

        [Test]
        public void CreateRawResultEntry_ForEachEntryInWord()
        {
            PerformTest("барашка")
                .Should()
                .ContainSingle()
                .Which.Entries
                .Should()
                .HaveCount(2);
        }

        private AnalysisResultRaw[] PerformTest(string input)
        {
            var result = analyser.GetRawResult(input);
            foreach(var entry in result)
                TestContext.Progress.WriteLine(entry.ToString());
            return result;
        }
    }
}