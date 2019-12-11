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
        private readonly IReader reader;
        private readonly IImageBuilder tagCloudImageCreator;
        private readonly ILayoutAlgorithm layoutAlgorithm;
        private readonly IImageSaver imageSaver;
        private readonly Filter filter;
        private readonly WordConverter wordConverter;

        public ConsoleUi(IReader reader,
            IImageBuilder tagCloudImageCreator, ILayoutAlgorithm layoutAlgorithm,
            IImageSaver imageSaver,
            Filter filter, WordConverter wordConverter)
        {
            this.filter = filter;
            this.wordConverter = wordConverter;
            this.reader = reader;
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

        private void Run(Options options)
        {
            Console.WriteLine(options.FontName);
            var words = reader.ReadWords(options.InputFile).Where(filter.FilterWord).Select(wordConverter.HandleWord);
            var tags = new List<Tag>();
            var frequencyDictionary = new FrequencyDictionary<string>(words);
            var top30 = frequencyDictionary.Top(30).ToArray();
            for (var i = 0; i < top30.Length; i++)
            {
                var (word, _) = top30[i];
                var size = TextRenderer.MeasureText(word, new Font(options.FontName, 40 - i));
                tags.Add(new Tag(word, layoutAlgorithm.PutNextRectangle(size), 40 - i));
            }

            var bitmap = tagCloudImageCreator.Build(options.FontName, tags, layoutAlgorithm.GetLayoutSize());
            imageSaver.Save(options.OutputFile, bitmap);
        }
    }
}