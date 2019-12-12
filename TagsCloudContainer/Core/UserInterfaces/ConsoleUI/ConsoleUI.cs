using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommandLine;
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
            Console.WriteLine(options.FontName);
            var reader = SelectReader(options.InputFile);
            if (reader == null)
                throw new ArgumentException("Формат входного файла не поддерживается");
            var words = reader.ReadWords(options.InputFile).Where(filter.FilterWord).Select(wordConverter.ConvertWord);
            var tags = new List<Tag>();
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
                frequencyDictionary.Add(word);
            var top30 = frequencyDictionary.Top(30).ToArray();
            for (var i = 0; i < top30.Length; i++)
            {
                var (word, _) = top30[i];
                var size = TextRenderer.MeasureText(word, new Font(options.FontName, 40 - i));
                tags.Add(new Tag(word, layoutAlgorithm.PutNextRectangle(size), 40 - i));
            }

            var bitmap = tagCloudImageCreator.Build(options.FontName, tags, layoutAlgorithm.GetLayoutSize());
            imageSaver.Save(options.OutputFile, bitmap, options.ImageFormat);
        }
    }
}