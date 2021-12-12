using System.Drawing;

namespace TagsCloudContainer.Layouter.PointsProviders
{
    public class SpiralPointsProviderFactory : IFactory<IPointsProvider>
    {
        private readonly ITagCloudSettings settings;

        public SpiralPointsProviderFactory(ITagCloudSettings settings)
        {
            this.settings = settings;
        }

        public IPointsProvider Create()
        {
            var center = new Point(settings.ImageWidth / 2, settings.ImageHeight / 2);
            return new SpiralPointsProvider(center);
        }
    }
}