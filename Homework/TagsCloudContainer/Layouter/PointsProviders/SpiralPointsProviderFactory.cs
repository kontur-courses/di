using System.Drawing;

namespace TagsCloudContainer.Layouter.PointsProviders
{
    public class SpiralPointsProviderFactory : IFactory<SpiralPointsProvider>
    {
        public SpiralPointsProvider Create(ITagCloudSettings settings)
        {
            var center = new Point(settings.ImageWidth / 2, settings.ImageHeight / 2);
            return new SpiralPointsProvider(center);
        }
    }
}