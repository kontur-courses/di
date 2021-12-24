using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualization.Statistics
{
    internal class LiteraryWordsStatistics : BaseWordsStatistics
    {
        public LiteraryWordsStatistics(LiteraryTextProcessor textProcessor) : base(textProcessor)
        {
        }

        public override IWordsStatistics CreateStatistics()
        {
            return new LiteraryWordsStatistics((LiteraryTextProcessor)TextProcessor);
        }
    }
}