using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.BitmapSaver;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.TextReader;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFrequencyAnalyzers;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private readonly IFileReader fileReader;
        private readonly ICloudLayouter layouter;
        private readonly IWordsPreparator wordsPreparator;
        private readonly IWordsFilter filter;
        private readonly IWordsFrequencyAnalyzer frequencyAnalyzer;
        private readonly IVisualizerSettings settings;
        private readonly IVisualizer visualizer;
        private readonly IBitmapSaver saver;

        public TagCloudCreator(IFileReader fileReader, ICloudLayouter layouter, IWordsPreparator wordsPreparator, IWordsFilter filter, IWordsFrequencyAnalyzer frequencyAnalyzer, IVisualizerSettings settings, IVisualizer visualizer, IBitmapSaver saver)
        {
            this.fileReader = fileReader;
            this.layouter = layouter;
            this.wordsPreparator = wordsPreparator;
            this.filter = filter;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.settings = settings;
            this.visualizer = visualizer;
            this.saver = saver;
        }

        public Bitmap CreateFromTextFile(string sourcePath)
        {
            var content = fileReader.ReadWords(sourcePath);
            var preparedWords = wordsPreparator.Prepare(content);
            var filteredWords = filter.Filter(preparedWords);
            var freqDict = frequencyAnalyzer.GetWordsFrequency(filteredWords);
            return visualizer.Visualize(freqDict);
        }

        public void CreateFromTextFileAndSave(string sourcePath, string fileName, string destinationPath = null, ImageFormat format = null)
        {
            if (destinationPath == null)
                destinationPath = Directory.GetCurrentDirectory();
            if (format == null)
                format = ImageFormat.Png;
            var destinationDirectory = new DirectoryInfo(destinationPath);
            saver.Save(CreateFromTextFile(sourcePath), destinationDirectory, fileName, format);
        }
    }
}