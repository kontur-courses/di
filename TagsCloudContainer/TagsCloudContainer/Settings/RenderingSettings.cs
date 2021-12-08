using System.Drawing;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class RenderingSettings : IRenderingSettings
    {
        public Size? DesiredImageSize
        {
            get => desiredImageSize;
            init
            {
                if (value.HasValue)
                {
                    Validate.Positive("Image height", value.Value.Height);
                    Validate.Positive("Image width", value.Value.Width);
                }

                desiredImageSize = value;
            }
        }

        public float Scale
        {
            get => scale;
            init => scale = Validate.Positive(nameof(Scale), value);
        }

        public Brush Background { get; init; } = new SolidBrush(Color.Transparent);

        private readonly float scale = 1;
        private readonly Size? desiredImageSize;

        public void Dispose()
        {
            Background.Dispose();
        }
    }
}