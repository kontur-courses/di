using System;
using System.IO;
using System.Linq;
using Autofac;
using NUnit.Framework;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Filters;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloudTests
{
    public class TextAnalyzerTests
    {
        private ContainerBuilder builder;
        private string myStemPath;
        [SetUp]
        public void Setup()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<TxtReader>().As<IReader<string>>();
            builder.RegisterType<WordAnalyzer>().As<ITokenAnalyzer<string>>();

            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
            
            var fileName = "mystem";
            myStemPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Release", fileName);
        }
        
        [TestCase(@"привет
я
дом
",
            new string[] {"привет", "дом"}, 
            TestName = "Filter SPRO")]
        [TestCase(@"машины
и
машина
",
            new string[] {"машина", "машина"}, 
            TestName = "Filter CONJ")]
        [TestCase(@"в
машина
",
            new string[] {"машина"}, 
            TestName = "Filter PR")]
        [TestCase(@"брошу
бросил
бросить
",
            new string[] {"бросать", "бросать", "бросать"}, 
            TestName = "Filter base form")]
        public void Parse_Interesting(string text, string[] expected)
        {
            builder.RegisterType<WordTypeFilter>()
                .As<IFilter<string>>()
                .WithParameter(new TypedParameter(typeof(string), myStemPath));
            builder.RegisterType<InterestingWordsFilter>().As<IFilter<string>>();
            Run(text, expected);
        }
        
        [TestCase(@"СЛОВО
Слово
слово
",
            new string[] {"слово", "слово", "слово"}, 
            TestName = "To Lower")]
        public void Parse_ToLower(string text, string[] expected)
        {
            builder.RegisterType<LowerCaseFilter>().As<IFilter<string>>();
            Run(text, expected);
        }

        private void Run(string text, string[] expected)
        {
            var container = builder.Build();
            var parser = container.Resolve<IReader<string>>();
            var analyzer = container.Resolve<ITokenAnalyzer<string>>();
            var settingsFactory = container.Resolve<Func<Settings>>();
            var path = Path.GetTempFileName();
            settingsFactory().ExcludedTypes = new [] {WordType.CONJ, WordType.SPRO, WordType.PR};
            settingsFactory().Path = path;
            File.WriteAllText(path, text);
            var tokens = parser.ReadTokens();

            var actual = analyzer.Analyze(tokens).Select(pair => pair.Item1);

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}