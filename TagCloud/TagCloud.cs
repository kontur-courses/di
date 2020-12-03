using TagCloud.Drawers;
using TagCloud.TextReaders;
using TagCloud.TextAnalyzer;
using TagCloud.ImageSavers;

namespace TagCloud
{
    public class TagCloud
    {
        private ITagDrawer drawer;
        private ITextReader textReader;
        private ITextAnalyzer textAnalyzer;
        private IImageSaver imageSaver;
        
        public TagCloud(ITagDrawer drawer, ITextReader textReader, ITextAnalyzer textAnalyzer, IImageSaver imageSaver)
        {
            this.drawer = drawer;
            this.textReader = textReader;
            this.textAnalyzer = textAnalyzer;
            this.imageSaver = imageSaver;
        }

        public void MakeTagCloud()
        {
            var words = textReader.ReadWords();
            var tags = textAnalyzer.GetTags(words);
            var result = drawer.DrawTagCloud(tags);
            imageSaver.Save(result);
            drawer.Dispose();
        }
    }
}