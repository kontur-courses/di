using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer
{
    [TestFixture]
    public class TagsCloudContainer_Should
    {
        private TagsCloudContainer container;
        private WordsRendererToImageDebug renderer;

        [Test]
        public void AutoSizeOutput()
        {
            renderer.AutoSize = true;
            container
                .AddFromText(GetRandomText(regularWords).WithOneWordPerLine())
                .Render();
            renderer.Output.Size.Should().Be(renderer.RenderingInfo.WordsBorders.Size.ToSize());
        }

        [Test]
        public void LowerCaseByDefault()
        {
            var text = GetRandomText(regularWords).WithLowerOrTitleCase().WithOneWordPerLine();
            container.AddFromText(text).Render();
            
            var resultWords = renderer.OutputInfo.Select(i => i.Word.Word);
            foreach (var word in resultWords) word.Should().Be(word.ToLower());
        }

        [Test]
        public void AllowPreprocessingForUpperCase()
        {
            var text = GetRandomText(regularWords).WithLowerOrTitleCase().WithOneWordPerLine();
            container.AddFromText(text)
                .Preprocessing(w => w.ToUpper())
                .Render();
            
            var resultWords = renderer.OutputInfo.Select(i => i.Word.Word);
            foreach (var word in resultWords) word.Should().Be(word.ToUpper());
        }
        
        [Test]
        public void AllowCustomPreprocessing()
        {
            var text = GetRandomText(regularWords).WithLowerOrTitleCase().WithOneWordPerLine();
            container.AddFromText(text)
                .Preprocessing(w => $"--{w}--")
                .Render();
            
            var resultWords = renderer.OutputInfo.Select(i => i.Word.Word);
            foreach (var word in resultWords) word.Should().StartWith("--").And.EndWith("--");
        }

        [Test]
        public void AllowExcludingWords()
        {
            var text = GetRandomText(regularWords, excludedWords).WithOneWordPerLine();
            container.AddFromText(text)
                .Excluding(excludedWords)
                .Render();
            var resultWords = renderer.OutputInfo.Select(i => i.Word.Word);
            resultWords.Should().NotContain(excludedWords);
        }

        [SetUp]
        public void SetUp()
        {
            renderer = new WordsRendererToImageDebug();
            container = new TagsCloudContainer()
                .Rendering(renderer);
        }

        private static TextBuilder GetRandomText(params string[][] words) => new TextBuilder(words);
        
        private string[] regularWords = {
            "массив", "набор", "куча", "обычных", "слов", "существительные", "прилагательные", "глаголы"
        };

        private string[] excludedWords = {
            "ты", "вы", "он", "она", "они"
        };
    }
}