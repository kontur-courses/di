using Autofac;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System.IO;
using System;

namespace TagsCloudContainer
{
    public abstract class TextHandler
    {
        protected readonly IDullWordsEliminator dullWordsEliminator;
        protected readonly ITextReader textReader;

        public TextHandler(ITextReader textReader, IDullWordsEliminator dullWordsEliminator)
        {
            this.dullWordsEliminator = dullWordsEliminator;
            this.textReader = textReader;
        }

        public abstract Dictionary<string, int> GetWordsFrequencyDict();

        [TestFixture]
        public class InjectionTest
        {
            [Test]
            public void TextHandlerInjections()
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(new TextFileReader("test")).As<ITextReader>();
                builder.RegisterInstance(new NothingDullEliminator())
                    .As<IDullWordsEliminator>();
                builder.RegisterType<DefaultTextHandler>().As<TextHandler>();
                var container = builder.Build();

                using (var scope = container.BeginLifetimeScope())
                {
                    var handler = scope.Resolve<TextHandler>();
                    handler.Should().BeOfType(typeof(DefaultTextHandler));
                    handler.textReader.Should().BeOfType(typeof(TextFileReader));
                    handler.dullWordsEliminator.Should().BeOfType(typeof(NothingDullEliminator));
                }
            }
        }
    }
}