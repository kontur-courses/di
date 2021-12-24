using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualization.Statistics
{
    internal class OneWordByLineStatistics : BaseWordsStatistics
    {
        public OneWordByLineStatistics(OneWordByLineProcessor textProcessor) : base(textProcessor)
        {
        }
        
        public override IWordsStatistics CreateStatistics()
        {
            return new OneWordByLineStatistics((OneWordByLineProcessor)TextProcessor);
        }
    }
}