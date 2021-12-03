using System;
using TagCloud.Analyzers;
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

        public ConsoleUI(IFileReader reader,
            ITextAnalyzer textAnalyzer,
            IFrequencyAnalyzer frequencyAnalyzer,
            ICloudLayouter layouter,
            IVisualizer visualizer,
            IFileWriter writer)
        {
            this.reader = reader;
            this.textAnalyzer = textAnalyzer;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.layouter = layouter;
            this.visualizer = visualizer;
            this.writer = writer;
        }

        public void Run(string filename)
        {
            var text = reader.ReadFile(filename);
            var words = textAnalyzer.Analyze(text);
            var wordFrequencies = frequencyAnalyzer.Analyze(words);
            var tags = layouter.PutTags(wordFrequencies);
            var image = visualizer.DrawCloud(tags);
            writer.Write(image);
        }
    }
}
