using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.CloudLayouter;
using TagCloud.CloudLayouter.CircularLayouter;
using TagCloud.TextColoration;
using TagCloud.TextConversion;
using TagCloud.TextFilterConditions;
using TagCloud.TextFiltration;
using TagCloud.TextParser;
using TagCloud.TextProvider;
using TagCloud.Visualization;

namespace TagCloud_Should
{
    [TestFixture]
    public class EndToEndTest
    {
        private ICloudLayouter cloudLayouter;
        private CloudVisualization cloudVisualization;
        private FrequencyDictionaryMaker frequencyDictionaryMaker;
        private TextConverter textConverter;

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BlacklistCondition>().As<IFilterCondition>();

            builder.RegisterType<RandomTextColoration>().As<ITextColoration>();
            builder.RegisterType<ToLowerCaseConversion>().As<ITextConversion>();

            builder.RegisterType<WordLengthCondition>().As<IFilterCondition, WordLengthCondition>().SingleInstance();
            builder.RegisterType<CloudVisualization>().AsSelf().SingleInstance();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<Size>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<ViewSettings>().AsSelf().SingleInstance();
            builder.RegisterType<TextFilter>().AsSelf().SingleInstance();
            builder.RegisterType<TextConverter>().AsSelf().SingleInstance();
            builder.RegisterType<TextParser>().As<ITextParser>().SingleInstance();
            builder.RegisterType<SpiralSettings>().AsSelf().SingleInstance();
            builder.RegisterType<ArchimedeanSpiral>().AsSelf().SingleInstance();
            builder.RegisterType<UnitTestsTextProvider>().As<ITextProvider, UnitTestsTextProvider>().SingleInstance();
            builder.RegisterType<FrequencyDictionaryMaker>().AsSelf().SingleInstance();
            builder.RegisterType<BlacklistMaker>().AsSelf().SingleInstance()
                .WithProperty("BlackList", new HashSet<string> {"blacklistWord"});
            builder.RegisterType<BlacklistSettings>().AsSelf().SingleInstance()
                .WithProperty("FilesWithBannedWords", new HashSet<string>());

            var container = builder.Build();
            cloudLayouter = container.Resolve<ICloudLayouter>();
            cloudVisualization = container.Resolve<CloudVisualization>();
            frequencyDictionaryMaker = container.Resolve<FrequencyDictionaryMaker>();
            textConverter = container.Resolve<TextConverter>();
        }

        [Test]
        public void EndToEndTest1()
        {
            cloudVisualization.Visualize();
            cloudLayouter.Rectangles.Count.Should().Be(12);
            var frequencyDictionary = frequencyDictionaryMaker.MakeFrequencyDictionary(textConverter.ConvertWords());
            frequencyDictionary.Count.Should().Be(12);
            frequencyDictionary.Select(x => x.Key).Should().BeEquivalentTo("word1", "word2", "word", "than", "more",
                "blacklistword", "word3", "the", "unit", "test", "are", "mad");
        }
    }
}