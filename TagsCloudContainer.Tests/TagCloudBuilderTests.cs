using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using TagsCloudContainer.ResultRenderer;
using TagsCloudContainer.WordFormatters;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsPreprocessors;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class TagCloudBuilderTests
    {
        private IWordsPreprocessor preprocessor1;
        private IWordsPreprocessor preprocessor2;
        private IWordFormatter formatter;
        private ILayouter layouter;
        private IResultRenderer renderer;
        private TagsCloudBuilder builder;
        private WordsSizer wordsSizer;

        [OneTimeSetUp]
        public void DoBeforeAllTest()
        {
            preprocessor1 = A.Fake<IWordsPreprocessor>();
            preprocessor2 = A.Fake<IWordsPreprocessor>();
            formatter = A.Fake<IWordFormatter>();
            layouter = A.Fake<ILayouter>();
            renderer = A.Fake<IResultRenderer>();
            wordsSizer = A.Fake<WordsSizer>();
            builder = new TagsCloudBuilder(
                new[] {preprocessor1, preprocessor2},
                formatter, layouter, renderer, wordsSizer);
        }

        [Test]
        public void Visualize_CallsDependenciesCorrectly()
        {
            var words = new[] {"a", "b", "c"};
            var formattedWords = new List<Word>
            {
                new Word(new Font(FontFamily.GenericMonospace, 12), Color.Black, "") {Position = new RectangleF()}
            };

            ConfigureFakes(words, formattedWords);
            builder.Visualize(words);
            AssertVisualizeSuccessful(words, formattedWords);
        }

        private void ConfigureFakes(IEnumerable<string> words, IEnumerable<Word> formattedWords)
        {
            A.CallTo(() => preprocessor1.Preprocess(words))
                .Returns(words);

            A.CallTo(() => preprocessor2.Preprocess(words))
                .Returns(words);

            A.CallTo(() => formatter.FormatWords(words))
                .Returns(formattedWords);

            A.CallTo(() => renderer.Generate(null))
                .WithAnyArguments()
                .Invokes(z => ((IEnumerable<Word>)z.Arguments[0]).ToList());
        }

        private void AssertVisualizeSuccessful(IEnumerable<string> words, IEnumerable<Word> formattedWords)
        {
            A.CallTo(() => preprocessor1.Preprocess(words))
                .MustHaveHappened(Repeated.Exactly.Once);

            A.CallTo(() => preprocessor2.Preprocess(words))
                .MustHaveHappened(Repeated.Exactly.Once);

            A.CallTo(() => formatter.FormatWords(words))
                .MustHaveHappened(Repeated.Exactly.Once);

            A.CallTo(() => layouter.GetNextPosition(new SizeF()))
                .MustHaveHappened(Repeated.Exactly.Times(formattedWords.Count()));

            A.CallTo(() => renderer.Generate(null))
                .WithAnyArguments()
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}