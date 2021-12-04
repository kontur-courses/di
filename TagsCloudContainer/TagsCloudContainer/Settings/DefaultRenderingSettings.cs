using System;
using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public interface IRenderingSettings
    {
        Size? DesiredImageSize { get; set; }
        float Scale { get; set; }
        Brush Background { get; set; }
    }

    public class DefaultRenderingSettings : IRenderingSettings
    {
        public Size? DesiredImageSize
        {
            get => desiredImageSize;
            set
            {
                if (value.HasValue)
                    if (value.Value.Height <= 0 || value.Value.Width <= 0)
                        throw new ApplicationException("Size must be positive.");

                desiredImageSize = value;
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                if (value <= 0)
                    throw new ApplicationException("Scale must be positive.");

                scale = value;
            }
        }

        public Brush Background { get; set; } = new SolidBrush(Color.Transparent);

        private float scale = 1;
        private Size? desiredImageSize;
    }
}