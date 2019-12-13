using System;

namespace TagsCloudVisualization.Layouters.CloudLayouters
{
    public class CloudLayouterConfiguration
    {
        public readonly Func<ICloudLayouter> GetCloudLayouter;

        public CloudLayouterConfiguration(Func<ICloudLayouter> getCloudLayouter)
        {
            GetCloudLayouter = getCloudLayouter;
        }
    }
}