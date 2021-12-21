using System.Collections.Generic;
using Autofac;
using Autofac.Core;
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
            TestName = "Stem check")]
        [TestCase("облако облака\r\nоблаком\tоблаках облаке", "облако", 5,
            TestName = "Ignore separators between words")]
        [TestCase("облако, облака. \"облаком\" - облаках! облаке;", "облако", 5,
            TestName = "Ignore punctuation chars between words")]
        [TestCase("Облако оБлакА облакоМ облаКах ОБЛАКЕ", "облако", 5,
            TestName = "Ignore case")]
        [TestCase("Облако в облаке на облаке за облаком об облаке", "облако", 5,
            TestName = "Ignore prepositions - Custom filter check")]
        [TestCase("Я Облако Ваше какое-то облако любое облако каким-нибудь облаком неким облаком", "облако", 5,
            TestName = "Ignore pronouns - Static filter check")]
        [TestCase("nag1bat0r nag1bat0r nag1bat0r nag1bat0r nag1bat0r", "nag1bat0r", 5,
            TestName = "Pass words when stem failed")]
        [TestCase("n.a,g:1{b)a!t?0'r", "n.a,g:1{b)a!t?0'r", 1,
            TestName = "Pass punctuation chars in word")]
        public void GetWordStatistics_ShouldWorksCorrectly(string text, string expectedStem, int stemCount)
        {
            var wordStatistics = textAnalyzer.GetWordStatistics(text);

            wordStatistics.Should().OnlyContain(stat => stat.Text == expectedStem && stat.Count == stemCount);
        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TextFileReader>().As<IFileReader>().AsSelf();
            builder.RegisterInstance(new Hunspell(DictRuAff, DictRuDic)).SingleInstance();
            builder.RegisterType<HunspellStemer>().As<IStemer>();
            builder.RegisterType<PronounFilter>().As<IWordFilter>();
            builder.RegisterType<CustomFilter>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IEnumerable<string>),
                    (pi, ctx) => ctx.Resolve<TextFileReader>().ReadLines(DictExcludeWords)))
                .As<IWordFilter>();
            builder.RegisterType<ComposeFilter>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IWordFilter[]),
                    (pi, ctx) => ctx.Resolve<IWordFilter[]>()));
            builder.RegisterType<TextAnalyzer>()
                .As<ITextAnalyzer>()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IWordFilter),
                    (pi, ctx) => ctx.Resolve<ComposeFilter>()));

            return builder.Build();
        }
    }
}