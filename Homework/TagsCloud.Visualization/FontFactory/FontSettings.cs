using System.Drawing;

namespace TagsCloud.Visualization.FontFactory
{
    public class FontSettings
    {
        public string FamilyName { get; init; } = "Times new roman";
        public int MaxSize { get; init; } = 2000;
        public FontStyle FontStyle { get; init; } = FontStyle.Regular;
    }
}