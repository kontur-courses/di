using System;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Drawers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Preprocessing;
using TagsCloudVisualization.Text;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Core
{
    public class TagCloudContainer
    {
        private readonly WordDrawer drawer;
        private readonly WordLayouter layouter;
        private readonly WordPainter painter;
        private readonly InputPreprocessor preprocessor;
        private readonly StatisticsCounter statCounter;
        private readonly ITextReader[] textReaders;

        public TagCloudContainer(ITextReader[] textReaders, InputPreprocessor preprocessor,
            StatisticsCounter statCounter, WordLayouter layouter,
            WordPainter painter, WordDrawer drawer)
        {
            this.textReaders = textReaders;
            this.preprocessor = preprocessor;
            this.statCounter = statCounter;
            this.layouter = layouter;
            this.painter = painter;
            this.drawer = drawer;
        }

        public Bitmap GetTagCloud(string filepath)
        {
            var textReader = FindTextReader(filepath);
            var words = textReader.GetAllWords(filepath);
            var preprocessedWords = preprocessor.PreprocessWords(words);
            var analyzedText = statCounter.GetAnalyzedText(preprocessedWords);
            var analyzedLayoutedText = layouter.GetLayoutedText(analyzedText);
            var paintedWords = painter.GetPaintedWords(analyzedLayoutedText);
            return drawer.GetDrawnLayoutedWords(paintedWords);
        }

        private ITextReader FindTextReader(string filepath)
        {
            var format = filepath.Split('.').LastOrDefault();

            if (format == null)
                throw new NullReferenceException("Text file was not specified.");
            format = format.ToLower();
            var reader = textReaders.FirstOrDefault(x => x.Formats.Contains(format));
            if (reader == null)
                throw new FormatException($"Format {format} is not supported");
            return reader;
        }
    }
}