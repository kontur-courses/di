using System.Drawing;

namespace TagCloud.configurations
{
    public class TagRepositoryRepositoryConfiguration : ITagRepositoryConfiguration
    {
        private readonly Color color;
        private readonly FontFamily fontFamily;
        private readonly float size;

        public TagRepositoryRepositoryConfiguration(Color color, FontFamily fontFamily, float size)
        {
            this.color = color;
            this.fontFamily = fontFamily;
            this.size = size;
        }

        public float GetSize() => size;

        public Color GetColor() => color;

        public FontFamily GetFamilyFont() => fontFamily;
    }
}