using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Reader;
using TagsCloudContainer.RectanglesTransformer;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordProcessor;
using TagsCloudContainer.WordsToSizesConverter;

namespace TagsCloudContainer
{
    public class TagsCloudCreator
    {
        private readonly ITextReader reader;
        private readonly IWordProcessor wordProcessor;
        private readonly IWordFrequenciesToSizesConverter converter;
        private readonly ILayouter layouter;
        private readonly IRectanglesTransformer rectanglesTransformer;
        private readonly IVisualizer visualizer;
        private readonly IImageSaver saver;

        public TagsCloudCreator(ITextReader reader, IWordProcessor wordProcessor, 
            IWordFrequenciesToSizesConverter converter, ILayouter layouter,
            IRectanglesTransformer rectanglesTransformer, IVisualizer visualizer, IImageSaver saver)
        {
            this.reader = reader;
            this.wordProcessor = wordProcessor;
            this.converter = converter;
            this.layouter = layouter;
            this.rectanglesTransformer = rectanglesTransformer;
            this.visualizer = visualizer;
            this.saver = saver;
        }

        public void CreateImage(string outputPath)
        {
            var words = reader.ReadWords();
            var wordsWithCount = wordProcessor.ProcessWords(words).ToList();
            var sizes = converter.ConvertToSizes(wordsWithCount);
            var rectangles = layouter.GetRectangles(sizes);
            rectangles = rectanglesTransformer.TransformRectangles(rectangles);
            words = wordsWithCount.Select(e => e.Word);
            var wordRectangles = words.Zip(rectangles, (w, r) => new WordRectangle(w, r)).ToList();
            var image = visualizer.DrawImage(wordRectangles);
            saver.SaveImage(image, outputPath);
        }
    }
}
