using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommandLine;
using TagsCloudContainer.Core.Extensions;
using TagsCloudContainer.Core.ImageBuilder;
using TagsCloudContainer.Core.ImageSavers;
using TagsCloudContainer.Core.LayoutAlgorithms;
using TagsCloudContainer.Core.Readers;
using TagsCloudContainer.Core.TextHandler.WordConverters;
using TagsCloudContainer.Core.TextHandler.WordFilters;

namespace TagsCloudContainer.Core.UserInterfaces.ConsoleUI
{
    class ConsoleUi : IUi
    {
        private readonly IReader[] readers;
        private readonly IImageBuilder tagCloudImageCreator;
        private readonly ILayoutAlgorithm layoutAlgorithm;
        private readonly IImageSaver imageSaver;
        private readonly Filter filter;
        private readonly WordConverter wordConverter;

        public ConsoleUi(IReader[] readers,
            IImageBuilder tagCloudImageCreator, ILayoutAlgorithm layoutAlgorithm,
            IImageSaver imageSaver,
            Filter filter, WordConverter wordConverter)
        {
            this.filter = filter;
            this.wordConverter = wordConverter;
            this.readers = readers;
            this.tagCloudImageCreator = tagCloudImageCreator;
            this.imageSaver = imageSaver;
            this.layoutAlgorithm = layoutAlgorithm;
        }

        public void Run(IEnumerable<string> userInput)
        {
            var options = Parser.Default
                .ParseArguments<Options>(userInput)
                .WithParsed(Run);
        }

        private IReader SelectReader(string path) => readers.FirstOrDefault(r => r.CanRead(path));

        private void Run(Options options)
        {
            var boringWords = FormBoringWords(options.FileWithBoringWords);
            filter.UserExcludedWords = boringWords;
            var words = FormWordsFromFile(options.InputFile)
                .MostCommon(30)
                .Select(kvp => kvp.Item1)
                .ToArray();

            var tags = FormTags(words, options.FontName);
            var bitmap = tagCloudImageCreator.Build(options.FontName, tags, layoutAlgorithm.GetLayoutSize());
            imageSaver.Save(options.OutputFile, bitmap, options.ImageFormat);
        }

        private IEnumerable<string> FormWordsFromFile(string path)
        {
            var reader = SelectReader(path);
            if (reader == null)
                throw new ArgumentException("Формат входного файла не поддерживается");
            return reader.ReadWords(path)
                .Where(filter.FilterWord)
                .Select(wordConverter.ConvertWord);
        }


        private HashSet<string> FormBoringWords(string path)
        {
            return path == null
                ? new HashSet<string>()
                : FormWordsFromFile(path).ToHashSet();
        }

        private IEnumerable<Tag> FormTags(IReadOnlyList<string> words, string font)
        {
            var tags = new List<Tag>();
            for (var i = 0; i < words.Count; i++)
            {
                var word = words[i];
                var size = TextRenderer.MeasureText(word, new Font(font, 40 - i));
                tags.Add(new Tag(word, layoutAlgorithm.PutNextRectangle(size), 40 - i));
            }

            return tags;
        }
    }
}