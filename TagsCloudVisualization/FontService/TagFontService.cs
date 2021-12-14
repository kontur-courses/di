using System.Drawing;

namespace TagsCloudVisualization.FontService
{
    internal class TagFontService : ITagFontService
    {
        private readonly string fontFamilyName;
        private readonly int maxFontSize;

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