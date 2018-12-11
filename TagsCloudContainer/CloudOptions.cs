namespace TagsCloudContainer
{
    public class CloudOptions
    {
        public readonly string Color;
        public readonly string FontFamily;
        public readonly string Format;
        public readonly string Height;
        public readonly string Width;

        public CloudOptions(string color, string fontFamily, string format, string height, string width)
        {
            this.Color = color;
            this.FontFamily = fontFamily;
            this.Format = format;
            this.Height = height;
            this.Width = width;
        }
    }
}
