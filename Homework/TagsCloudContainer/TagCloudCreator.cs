using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.BitmapSaver;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFrequencyAnalyzers;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer
{
    public class TagCloudCreator
    {
        private readonly IFileReader fileReader;
        private readonly IWordsConverter wordsConverter;
        private readonly IWordsFilter filter;
        private readonly IWordsFrequencyAnalyzer frequencyAnalyzer;
        private readonly IVisualizer visualizer;
        private readonly IBitmapSaver saver;

        public TagCloudCreator(IFileReader fileReader,
            IWordsConverter wordsConverter,
            IWordsFilter filter,
            IWordsFrequencyAnalyzer frequencyAnalyzer,
            IVisualizer visualizer, 
            IBitmapSaver saver)
        {
            this.fileReader = fileReader;
            this.wordsConverter = wordsConverter;
            this.filter = filter;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.visualizer = visualizer;
            this.saver = saver;
        }

        public Bitmap CreateFromTextFile(string sourcePath)
        {
            var content = fileReader.ReadWords(sourcePath);
            var preparedWords = wordsConverter.Convert(content);
            var filteredWords = filter.Filter(preparedWords);
            var freqDict = frequencyAnalyzer.GetWordsFrequency(filteredWords);
            return visualizer.Visualize(freqDict);
        }

        public void CreateFromTextFileAndSave(string sourcePath, string fileName, string? destinationPath = null, ImageFormat? format = null)
        {
            destinationPath ??= Directory.GetCurrentDirectory();
            format ??= ImageFormat.Png;
            var destinationDirectory = new DirectoryInfo(destinationPath);
            saver.Save(CreateFromTextFile(sourcePath), destinationDirectory, fileName, format);
        }
    }
}