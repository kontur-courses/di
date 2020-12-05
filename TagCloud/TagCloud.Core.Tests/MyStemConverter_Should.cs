using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Text.Preprocessing;

namespace TagCloud.Core.Tests
{
    // ReSharper disable once InconsistentNaming
    public class MyStemConverter_Should
    {
        private string executablePath;
        private MyStemWordsConverter converter;

        [SetUp]
        public void SetUp()
        {
            executablePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "mystem.exe");
            converter = new MyStemWordsConverter();
        }

        [Test]
        public void MyStemExeMissing_Throw()
        {
            var temporaryPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "totally_not_a_mystem.exe");
            File.Move(executablePath, temporaryPath, true);

            Action test = () => converter.Normalize(new[] {"abc"});

            test.Should()
                .Throw<FileNotFoundException>()
                .Which
                .Message
                .Should()
                .Contain("Missed mystem library");

            File.Move(temporaryPath, executablePath);
        }

        [Test]
        public void SingleWord_Normalize()
        {
            converter.Normalize(new[] {"упячкой"})
                .Should()
                .BeEquivalentTo("упячка");
        }

        [Test]
        public void SeveralWords_NormalizeEach()
        {
            converter.Normalize(new[] {"упячкой", "бошечки", "стирателей"})
                .Should()
                .BeEquivalentTo("упячка", "бошечка", "стиратель");
        }

        [Test]
        public void AlreadyNormalized_DontModify()
        {
            converter.Normalize(new[] {"упячка"})
                .Should()
                .BeEquivalentTo("упячка");
        }

        [Test]
        public void EnglishWords_DontModify()
        {
            converter.Normalize(new[] {"matches"})
                .Should()
                .BeEquivalentTo("matches");
        }
    }
}