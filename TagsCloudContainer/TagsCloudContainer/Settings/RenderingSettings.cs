using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public class RenderingSettings : IRenderingSettings
    {
        public Size? DesiredImageSize { get; }
        public float Scale { get; }
        public Brush Background { get; }

        public RenderingSettings(IRenderSettings settings)
        {
            DesiredImageSize = ValidateSize(settings.ImageSize);
            Scale = Validate.Positive("Image scale", settings.ImageScale);
            Background = new SolidBrush(settings.BackgroundColor);
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