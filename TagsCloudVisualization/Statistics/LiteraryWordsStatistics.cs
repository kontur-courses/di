using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualization.Statistics
{
    public class LiteraryWordsStatistics : BaseWordsStatistics
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