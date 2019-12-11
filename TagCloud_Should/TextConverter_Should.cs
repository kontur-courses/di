using System.Collections.Generic;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.TextConversion;
using TagCloud.TextFilterConditions;
using TagCloud.TextFiltration;
using TagCloud.TextParser;
using TagCloud.TextProvider;

namespace TagCloud_Should
{
    [TestFixture]
    public class TextConverter_Should
    {
        private TextConverter textConverter;

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ToLowerCaseConversion>().As<ITextConversion>();
            builder.RegisterType<WordLengthCondition>().As<IFilterCondition>().SingleInstance();
            builder.RegisterType<TextFilter>().AsSelf().SingleInstance();
            builder.RegisterType<TextConverter>().AsSelf().SingleInstance();
            builder.RegisterType<TextParser>().As<ITextParser>().SingleInstance();
            builder.RegisterType<UnitTestsTextProvider>().As<ITextProvider>().SingleInstance();
            builder.RegisterType<FrequencyDictionaryMaker>().AsSelf().SingleInstance();
            builder.RegisterType<BlacklistMaker>().AsSelf().SingleInstance()
                .WithProperty("BlackList", new HashSet<string> {"blacklistWord"});
            builder.RegisterType<BlacklistSettings>().AsSelf().SingleInstance()
                .WithProperty("FilesWithBannedWords", new HashSet<string>());

            var container = builder.Build();
            textConverter = container.Resolve<TextConverter>();
        }

        [Test]
        public void ShouldConvertToLower()
        {
            textConverter.ConvertWords().Should().BeEquivalentTo(new List<string>
            {
                "word1", "word2", "word", "than", "more", "word", "word1", "word", "word1", "word1", "word1", "word2",
                "word2", "word2", "blacklistword", "blacklistword", "word3", "blacklistword", "word", "the", "word",
                "word", "unit", "test", "are", "mad"
            });
        }
    }
}