using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.Layouter.Spirals
{
    public class SpiralFactory
    {
        public ISpiral Create(LayouterSettings layouterSettings)
        {
            switch (layouterSettings.SpiralType)
            {
                case SpiralType.Ferma:
                    return new FermaSpiral(layouterSettings.Center, layouterSettings.SpiralCoefficient);
                case SpiralType.InLine:
                    return new InLineSpiral(layouterSettings.Center, layouterSettings.SpiralCoefficient);
                default:
                    return new RectangleSpiral(layouterSettings.Center, layouterSettings.SpiralCoefficient);
            }
        }
    }
}