using System.Drawing;
using TagCloud.configurations;

namespace TagCloud
{
    public class Tag
    {
        private readonly string text;
        private readonly ITagConfiguration configuration;
        private readonly RectangleF layoutRectangle;

        public Tag(string text, ITagConfiguration tagConfiguration, RectangleF layoutRectangle)
        {
            this.text = text;
            configuration = tagConfiguration;
            this.layoutRectangle = layoutRectangle;
        }

        public string GetText() => text;

        public ITagConfiguration GetConfiguration() => configuration;

        public RectangleF GetLayoutRectangle() => layoutRectangle;
    }
}