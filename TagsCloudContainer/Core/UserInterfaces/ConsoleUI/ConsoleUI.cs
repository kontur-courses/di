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
using TagsCloudContainer.Core.TextHandler.WordFilters;
using TagsCloudContainer.Core.TextHandler.WordHandlers;

namespace TagsCloudContainer.Core.UserInterfaces.ConsoleUI
{
    class ConsoleUi : IUi
    {
        private readonly IReader reader;
        private readonly IImageBuilder tagCloudImageCreator;
        private readonly ILayoutAlgorithm layoutAlgorithm;
        private readonly IImageSaver imageSaver;
        private readonly Filter filter;
        private readonly Handler handler;

        public ConsoleUi(IEnumerable<string> userInput, IReader reader,
            IImageBuilder tagCloudImageCreator, ILayoutAlgorithm layoutAlgorithm,
            IImageSaver imageSaver,
            Filter filter, Handler handler)
        {
            this.filter = filter;
            this.handler = handler;
            this.reader = reader;
            this.tagCloudImageCreator = tagCloudImageCreator;
            this.imageSaver = imageSaver;
            this.layoutAlgorithm = layoutAlgorithm;
            Parser.Default
                .ParseArguments<Options>(userInput)
                .WithParsed(Run);

        }

        public void Run(Options options)
        {
            Console.WriteLine(options.FontName);
            var words = reader.ReadWords(options.InputFile).Where(filter.FilterWord).Select(handler.HandleWord);
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