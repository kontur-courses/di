using Autofac;
using FluentAssertions;
using NHunspell;
using NUnit.Framework;
using TagsCloudVisualization.Common.FileReaders;
using TagsCloudVisualization.Common.Stemers;
using TagsCloudVisualization.Common.TextAnalyzers;
using TagsCloudVisualization.Common.WordFilters;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TextAnalyzerTests
    {
        private const string DictRuAff = @"dicts\ru.aff";
        private const string DictRuDic = @"dicts\ru.dic";
        private const string DictExcludeWords = @"filters\excludeWords.txt";
        private ITextAnalyzer textAnalyzer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            textAnalyzer = ConfigureContainer().Resolve<ITextAnalyzer>();
        }
        
        [TestCase("облако облака облаком облаках облаке", "облако", 5,
            TestName = "StemCheck")]
        [TestCase("облако облака\r\nоблаком\tоблаках облаке", "облако", 5,
            TestName = "IgnoreSeparators")]
        [TestCase("облако, облака. \"облаком\" - облаках! облаке;", "облако", 5,
            TestName = "IgnorePunctuationChars")]
        [TestCase("Облако оБлакА облакоМ облаКах ОБЛАКЕ", "облако", 5,
            TestName = "IgnoreCase")]
        [TestCase("Облако в облаке на облаке за облаком об облаке", "облако", 5,
            TestName = "IgnorePrepositions - CustomFilterCheck")]
        [TestCase("Я Облако Ваше какое-то облако любое облако каким-нибудь облаком неким облаком", "облако", 5,
            TestName = "IgnorePronouns - StaticFilterCheck")]
        [TestCase("nag1bat0r nag1bat0r nag1bat0r nag1bat0r nag1bat0r", "nag1bat0r", 5,
            TestName = "PassWords_WhenStemFailed")]
        [TestCase("n.a,g:1{b)a!t?0'r", "n.a,g:1{b)a!t?0'r", 1,
            TestName = "PassPunctuationChars_WhenInWord")]
        public void GetWordStatistics_ShouldWorksCorrectly(string text, string expectedStem, int stemCount)
        {
            var stat = textAnalyzer.GetWordStatistics(text);
            stat.Should().OnlyContain(pair => pair.Key == expectedStem && pair.Value == stemCount);
        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<TextFileReader>().As<IFileReader>();
            builder.RegisterInstance(new Hunspell(DictRuAff, DictRuDic)).SingleInstance();
            builder.RegisterType<HunspellStemer>().As<IStemer>();
            builder.RegisterType<PronounFilter>().As<IWordFilter>();
            builder.RegisterType<CustomFilter>()
                .As<IWordFilter>()
                .OnActivated(service => service.Instance.Load(DictExcludeWords));
            builder.RegisterType<TextAnalyzer>().As<ITextAnalyzer>();

            return builder.Build();
        }
    }
}