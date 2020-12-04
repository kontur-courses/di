using System;
using System.Drawing;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class CloudLayouterFactory : ICloudLayouterFactory
    {
        public ICloudLayouter CreateCloudLayouter(CloudLayouterAlgorithm algorithm, Size imageSize)
        {
            switch (algorithm)
            {
                case CloudLayouterAlgorithm.CircularCloudLayouter:
                    return new CircularCloudLayouter(new Point(imageSize.Width / 2, imageSize.Height / 2));
            }

            throw new NotImplementedException("No found this algorithm");
        }
    }
}