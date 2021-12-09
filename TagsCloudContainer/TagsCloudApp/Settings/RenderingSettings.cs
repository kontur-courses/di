using System.Drawing;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class RenderingSettings : IRenderingSettings
    {
        public Size? DesiredImageSize { get; }
        public float Scale { get; }
        public Brush Background { get; }

        public RenderingSettings(IRenderOptions renderOptions, IArgbColorParser colorParser)
        {
            DesiredImageSize = ValidateSize(renderOptions.ImageSize);
            Scale = Validate.Positive("Image scale", renderOptions.ImageScale);
            var color = colorParser.Parse(renderOptions.BackgroundColor);
            Background = new SolidBrush(color);
        }

        private static Size? ValidateSize(Size? size)
        {
            if (!size.HasValue)
                return size;

            Validate.Positive("Image height", size.Value.Height);
            Validate.Positive("Image width", size.Value.Width);

            return size;
        }

        public void Dispose()
        {
            Background.Dispose();
        }
    }
}