using System;
using System.Drawing.Imaging;
using TagCloud.Analyzers;
using TagCloud.Creators;
using TagCloud.Layouters;
using TagCloud.Readers;
using TagCloud.Visualizers;
using TagCloud.Writers;

namespace TagCloud.UI
{
    public class ConsoleUI : IUserInterface
    {
        private readonly IFileReader reader;
        private readonly ITextAnalyzer textAnalyzer;
        private readonly IFrequencyAnalyzer frequencyAnalyzer;
        private readonly ICloudLayouter layouter;
        private readonly IVisualizer visualizer;
        private readonly IFileWriter writer;
        private readonly ITagCreator tagCreator;

        public ConsoleUI(IFileReader reader,
            ITextAnalyzer textAnalyzer,
            IFrequencyAnalyzer frequencyAnalyzer,
            ICloudLayouter layouter,
            IVisualizer visualizer,
            IFileWriter writer, 
            ITagCreator tagCreator)
        {
            this.reader = reader;
            this.textAnalyzer = textAnalyzer;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.layouter = layouter;
            this.visualizer = visualizer;
            this.writer = writer;
            this.tagCreator = tagCreator;
        }

        public void Run(string filename)
        {
            var text = reader.ReadFile(filename);
            var words = textAnalyzer.Analyze(text);
            var wordFrequencies = frequencyAnalyzer.Analyze(words);
            var tags = tagCreator.Create(wordFrequencies);
            var placedTags = layouter.PutTags(tags);
            var image = visualizer.DrawCloud(placedTags);
            writer.Write(image, filename, ImageFormat.Png);
        }
    }
}
