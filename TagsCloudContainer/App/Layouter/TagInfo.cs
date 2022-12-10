using System.Drawing;

namespace TagsCloudContainer.App.Layouter
{
    public class TagInfo
    {
        public string TagText { get; set; }
        public Font TagFont { get; set; }
        public Rectangle TagRect { get; set; }

        public TagInfo(string tagText, Font tagFont)
        {
            TagText = tagText;
            TagFont = tagFont;
        }
    }
}