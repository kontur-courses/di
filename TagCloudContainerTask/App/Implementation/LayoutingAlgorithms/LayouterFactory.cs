using System.Drawing;
using App.Implementation.LayoutingAlgorithms.AlgorithmFromTDD;
using App.Infrastructure.LayoutingAlgorithms;
using App.Infrastructure.LayoutingAlgorithms.AlgorithmFromTDD;
using App.Infrastructure.SettingsHolders;

namespace App.Implementation.LayoutingAlgorithms
{
    public class LayouterFactory : ILayouterFactory
    {
        private readonly IImageSizeSettingsHolder imageSizeSettings;

        public LayouterFactory(IImageSizeSettingsHolder imageSizeSettings)
        {
            this.imageSizeSettings = imageSizeSettings;
        }

        public ICloudLayouter CreateLayouter()
        {
            return new CircularLayouter(
                new Point(
                    imageSizeSettings.Size.Width / 2,
                    imageSizeSettings.Size.Height / 2)
            );
        }
    }
}