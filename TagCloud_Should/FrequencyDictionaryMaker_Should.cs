using System.Collections.Generic;
using System.Linq;
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
    public class FrequencyDictionaryMaker_Should
    {
        private FrequencyDictionaryMaker frequencyDictionaryMaker;
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
            frequencyDictionaryMaker = container.Resolve<FrequencyDictionaryMaker>();
            textConverter = container.Resolve<TextConverter>();
        }

        [Test]
        public void ShouldNotBeNull()
        {
            frequencyDictionaryMaker
                .MakeFrequencyDictionary(textConverter.ConvertWords())
                .Should().NotBeNull();
        }

        [Test]
        public void ShouldNotContainZeroFrequency()
        {
            frequencyDictionaryMaker
                .MakeFrequencyDictionary(textConverter.ConvertWords())
                .Select(w => w.Value)
                .Should().NotContain(0);
        }
    }
}