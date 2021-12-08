using System.Drawing;

namespace TagsCloudVisualization.FontService
{
    public class TagFontService : ITagFontService
    {
        private readonly int maxFontSize;
        private readonly string fontFamilyName;

        public TagFontService(int maxFontSize, string fontFamilyName)
        {
            this.maxFontSize = maxFontSize;
            this.fontFamilyName = fontFamilyName;
        }

        public Font GetFont(Tag tag, float minCount, float maxCount)
        {
            var fontSize = Normalize(tag.Count, minCount, maxCount);
            return new Font(fontFamilyName, fontSize);
        }
        
        private float Normalize(float count, float minCount, float maxCount) => 
            count <= minCount ? 1 : maxFontSize * (count - minCount) / (maxCount - minCount);
    }
}