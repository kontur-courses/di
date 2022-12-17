using System.Drawing;

namespace TagsCloudContainer
{
    public class CircularCloudSettingsProvider : ISettingsProvider
    {
        public CircularCloudSettings CircularCloudSettings { get; }

        public CircularCloudSettingsProvider()
        {
            CircularCloudSettings = new CircularCloudSettings()
            {
                StepSize = 10
            };
        }
    }
}