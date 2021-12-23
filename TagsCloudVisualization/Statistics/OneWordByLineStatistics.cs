using TagsCloudVisualization.WordProcessors;

namespace TagsCloudVisualization.Statistics
{
    public class OneWordByLineStatistics : BaseWordsStatistics
    {
        public OneWordByLineStatistics(OneWordByLineProcessor textProcessor) : base(textProcessor)
        {
        }
        
        public override IWordsStatistics CreateStatistics()
        {
            return new OneWordByLineStatistics((OneWordByLineProcessor)textProcessor);
        }
    }
}