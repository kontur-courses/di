using System;
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
                    if (value.Value.Height <= 0 || value.Value.Width <= 0)
                        throw new ApplicationException("Size must be positive.");

                desiredImageSize = value;
            }
        }

        public float Scale
        {
            get => scale;
            init
            {
                if (value <= 0)
                    throw new ApplicationException("Scale must be positive.");

                scale = value;
            }
        }

        public Brush Background { get; init; } = new SolidBrush(Color.Transparent);

        private readonly float scale = 1;
        private readonly Size? desiredImageSize;

        public void Dispose()
        {
            Background?.Dispose();
        }
    }
}