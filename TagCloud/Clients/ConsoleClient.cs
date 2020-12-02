using TagCloud.TextConverters.TextReaders;
using TagCloud.TextConverters.TextProcessors;
using TagCloud.WordsMetrics;
using TagCloud.PointGetters;
using TagCloud.Visualization;

namespace TagCloud.Clients
{
    internal class ConsoleClient : IClient
    {
        private readonly ITextReader reader;
        private readonly ITextProcessor processor;
        private readonly IWordsMetric metric;
        private readonly IPointGetter pointGetter;

        internal ConsoleClient(ITextReader reader, ITextProcessor processor, 
            IWordsMetric metric, IPointGetter pointGetter)
        {
            this.reader = reader;
            this.processor = processor;
            this.metric = metric;
            this.pointGetter = pointGetter;
        }

        public void Run()
        {

        }
        public void Visualization(string wordsPath, string picturePath, VisualizationInfo info)
        {
            if (!wordsPath.EndsWith(reader.Extension))
                return;
            var text = reader.ReadText(wordsPath);
            var tagCloud = AlgoritmTagCloud.GetTagCloud(text, pointGetter, processor, metric);
            TagCloudVisualization.Visualization(tagCloud, picturePath, info);
        }
    }
}
