using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using NUnit.Framework;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Conveyors;
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
            new[] {"привет", "дом"}, 
            TestName = "Filter SPRO")]
        [TestCase(@"машины
и
машина
",
            new[] {"машина", "машина"}, 
            TestName = "Filter CONJ")]
        [TestCase(@"в
машина
",
            new[] {"машина"}, 
            TestName = "Filter PR")]
        [TestCase(@"брошу
бросил
бросить
",
            new[] {"бросать", "бросать", "бросать"}, 
            TestName = "Filter base form")]
        public void Parse_Interesting(string text, string[] expected)
        {
            builder.RegisterType<WordTypeConveyor>()
                .As<IConveyor<string>>()
                .WithParameter(new TypedParameter(typeof(string), myStemPath));
            builder.RegisterType<InterestingWordsConveyor>().As<IConveyor<string>>();
            Run(text, expected);
        }
        
        [TestCase(@"СЛОВО
Слово
слово
",
            new[] {"слово", "слово", "слово"}, 
            TestName = "To Lower")]
        public void Parse_ToLower(string text, string[] expected)
        {
            builder.RegisterType<LowerCaseConveyor>().As<IConveyor<string>>();
            Run(text, expected);
        }

        private void Run(string text, string[] expected)
        {
            var container = builder.Build();
            var parser = container.Resolve<IReader<string>>();
            var settingsFactory = container.Resolve<Func<Settings>>();
            var path = Path.GetTempFileName();
            settingsFactory().ExcludedTypes = new [] {WordType.CONJ, WordType.SPRO, WordType.PR};
            settingsFactory().Path = path;
            File.WriteAllText(path, text);
            var tokens = parser.ReadTokens();
            var conveyors = container.Resolve<IEnumerable<IConveyor<string>>>();
            var actual = Analyze(conveyors, tokens).Select(pair => pair.Item1);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        private IEnumerable<(string, TokenInfo)> Analyze(IEnumerable<IConveyor<String>> conveyors, IEnumerable<String> tokens)
        {
            return conveyors.Aggregate(
                tokens.Select(line => (line, new TokenInfo())),
                (current, filter) => filter.Handle(current).ToArray());
        }
    }
}