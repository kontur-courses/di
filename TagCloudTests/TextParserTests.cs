using System;
using System.IO;
using Autofac;
using NUnit.Framework;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Filters;

namespace TagCloudTests
{
    public class TextParserTests
    {
        private ContainerBuilder builder;
        [SetUp]
        public void Setup()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<LineParser>().As<IParser<string>>();
            
            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
        
        [TestCase(@"привет
я
дом
",
            new string[] {"дом"}, 
            TestName = "Filter SPRO")]
        [TestCase(@"в
машина
",
            new string[] {"машина"}, 
            TestName = "Filter CONJ")]
        [TestCase(@"брошу
бросил
бросить
",
            new string[] {"бросать", "бросать", "бросать"}, 
            TestName = "Filter base form")]
        public void Parse_Interesting(string text, string[] expected)
        {
            var fileName = "mystem";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "TagCloud", "bin", "Debug", "net48", fileName);
            builder.RegisterType<InterestingWordsFilter>()
                .As<IFilter<string>>()
                .WithParameter(new TypedParameter(typeof(string), path));
            Parse(text, expected);
        }
        
        [TestCase(@"СЛОВО
Слово
слово
",
            new string[] {"слово", "слово", "слово"}, 
            TestName = "To Lower")]
        public void Parse_ToLower(string text, string[] expected)
        {
            builder.RegisterType<ToLowerFilter>().As<IFilter<string>>();
            Parse(text, expected);
        }

        private void Parse(string text, string[] expected)
        {
            var container = builder.Build();
            var parser = container.Resolve<IParser<string>>();
            var settingsFactory = container.Resolve<Func<Settings>>();
            var path = Path.GetTempFileName();
            settingsFactory().ExcludedTypes = new []{"CONJ", "SPRO"};
            settingsFactory().Path = path;
            File.WriteAllText(path, text);

            var actual = parser.Parse();
            
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}