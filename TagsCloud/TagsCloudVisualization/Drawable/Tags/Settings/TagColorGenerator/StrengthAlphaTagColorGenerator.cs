using System;
using System.Drawing;

namespace TagsCloudVisualization.Drawable.Tags.Settings.TagColorGenerator
{
    public class StrengthAlphaTagColorGenerator : ITagColorGenerator
    {
        private readonly Color _baseColor;

        public StrengthAlphaTagColorGenerator(Color baseColor)
        {
            _baseColor = baseColor;
        }

        public Color Generate(Tag tag)
        {
            var alpha = Convert.ToInt32(byte.MaxValue * tag.Weight);
            return Color.FromArgb(alpha, _baseColor);
        }
    }
}