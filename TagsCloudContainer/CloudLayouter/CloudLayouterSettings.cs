using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.CloudLayouter
{
    public class CloudLayouterSettings : ICloudLayouterSettings
    {
        public int ImageWidth { get; }
        public int ImageHeight { get; }
        public int CenterX { get; }
        public int CenterY { get; }
        public int RotationAngle { get; }

        public CloudLayouterSettings(IConfiguration configuration)
        {
            ImageWidth = configuration.ImageWidth;
            ImageHeight = configuration.ImageHeight;
            CenterX = configuration.CenterX;
            CenterY = configuration.CenterY;
            RotationAngle = configuration.RotationAngle;
        }
    }
}