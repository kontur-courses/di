using System.Drawing;

namespace TagsCloudContainer.Infrastructure.CloudGenerator
{
    internal interface ICloudLayouterFactory
    {
        public ICloudLayouter CreateCloudLayouter(CloudLayouterAlgorithm algorithm, Size imageSize);
    }
}