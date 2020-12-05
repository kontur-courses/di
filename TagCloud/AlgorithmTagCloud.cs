using TagCloud.PointGetters;
using TagCloud.WordsMetrics;
using TagCloud.TextConverters.TextProcessors;

namespace TagCloud
{
    public static class AlgorithmTagCloud
    {
        internal static TagCloud GetTagCloud(string text, IPointGetter pointGetter, 
            ITextProcessor textProcessor, IWordsMetric wordsMetric)
        {
            var metric = wordsMetric.GetMetric(textProcessor.GetLiterals(text));
            return new TagCloud(metric, pointGetter);
        }
    }
}
