using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.ImageSizeCalculator;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Reader;
using TagsCloudContainer.RectanglesTransformer;
using TagsCloudContainer.UI;
using TagsCloudContainer.UI.SettingsCommands;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;

namespace TagsCloudContainer.ImageCreator
{
    public class ImageCreator : IImageCreator
    {
        private readonly ITextReader reader;
        private readonly IWordProcessor wordProcessor;
        private readonly IWordFrequenciesToSizesConverter converter;
        private readonly ILayouter layouter;
        private readonly IImageSizeCalculator imageSizeCalculator;
        private readonly IRectanglesTransformer rectanglesTransformer;
        private readonly IVisualizer visualizer;
        private readonly IImageSaver saver;

        public ImageCreator(ITextReader reader, IWordProcessor wordProcessor,
            IWordFrequenciesToSizesConverter converter, ILayouter layouter, IImageSizeCalculator imageSizeCalculator,
            IRectanglesTransformer rectanglesTransformer, IVisualizer visualizer, IImageSaver saver)
        {
            this.reader = reader;
            this.wordProcessor = wordProcessor;
            this.converter = converter;
            this.layouter = layouter;
            this.imageSizeCalculator = imageSizeCalculator;
            this.rectanglesTransformer = rectanglesTransformer;
            this.visualizer = visualizer;
            this.saver = saver;
        }

        public void CreateImage(IInitialSettings settings)
        {
            var words = reader.ReadWords(settings.InputFilePath);
            var wordsWithCount = wordProcessor.ProcessWords(words).ToList();
            var sizes = converter.ConvertToSizes(wordsWithCount);
            var rectangles = layouter.GetRectangles(sizes);
            var imageSize = settings.ImageSize;
            if (imageSize == Size.Empty)
                imageSize = imageSizeCalculator.CalculateImageSize(rectangles);
            rectangles = rectanglesTransformer.TransformRectangles(rectangles, imageSize);
            words = wordsWithCount.Select(e => e.Word);
            var wordRectangles = words.Zip(rectangles, (w, r) => new WordRectangle(w, r)).ToList();
            var image = visualizer.DrawImage(wordRectangles, imageSize);
            saver.SaveImage(image, settings.OutputFilePath);
        }
    }
}
