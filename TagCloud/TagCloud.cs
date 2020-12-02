using TagCloud.Layout;
using TagCloud.TextReaders;
using TagCloud.TextAnalyzer;

namespace TagCloud
{
    public class TagCloud
    {
        private ITagCloudLayout layout;
        private ITextReader textReader;
        private ITextAnalyzer textAnalyzer;
        
        public TagCloud(ITagCloudLayout layout, ITextReader textReader, ITextAnalyzer textAnalyzer)
        {
            this.layout = layout;
            this.textReader = textReader;
            this.textAnalyzer = textAnalyzer;
        }

        public void MakeTagCloud()
        {
            var words = textReader.ReadWords();
            var tags = textAnalyzer.GetTags(words);
            layout.DrawTagCloud(tags);
        }
    }
}