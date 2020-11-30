using TagCloud.PointGetters;
using TagCloud.WordsMetrics;
using TagCloud.TextConverters.TextProcessors;

namespace TagCloud
{
    public static class AlgoritmTagCloud
    {
        internal static TagCloud GetTagCloud(string text, IPointGetter pointGetter, 
            ITextProcessor textProcessor, IWordsMetric wordsMetric)
        {
            var metric = wordsMetric.Process(textProcessor.Process(text));
            return new TagCloud(metric, pointGetter);
        }
    }
}
