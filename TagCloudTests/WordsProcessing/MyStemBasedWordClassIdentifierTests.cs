using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure;
using TagCloud.WordsProcessing;

namespace TagCloudTests.WordsProcessing
{
    public class MyStemBasedWordClassIdentifierTests
    {
        private readonly string myStemPath = 
            $"{Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName}/mystem.exe";

        private MyStemBasedWordClassIdentifier wordClassIdentifier;

        [SetUp]
        public void SetUp()
        {
            wordClassIdentifier = new MyStemBasedWordClassIdentifier(myStemPath);
        }

        [TestCase("хлеб", WordClass.Noun, TestName = "Noun")]
        [TestCase("горячий", WordClass.Adjective, TestName = "Adjective")]
        [TestCase("далеко", WordClass.Adverb, TestName = "Adverb")]
        [TestCase("он", WordClass.Pronoun, TestName = "Pronoun")]
        [TestCase("первый", WordClass.Numeral, TestName = "Numeral")]
        [TestCase("и", WordClass.Conjunction, TestName = "Conjunction")]
        [TestCase("ах", WordClass.Interjection, TestName = "Interjection")]
        [TestCase("не", WordClass.Particle, TestName = "Particle")]
        [TestCase("под", WordClass.Preposition, TestName = "Preposition")]
        [TestCase("готовить", WordClass.Verb, TestName = "Verb")]
        public void GetWordClass_ShouldWorkCorrectly_OnWordClass(string word, WordClass expectedWordClass)
        {
            wordClassIdentifier.GetWordClass(word).Should().Be(expectedWordClass);
        }
    }
}
