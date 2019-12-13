using System;
using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.Layouter.Spirals
{
    public class SpiralFactory
    {
        public ISpiral Create(LayouterSettings layouterSettings)
        {
            //Have no idea how to change to polymorphism
            switch (layouterSettings.SpiralType)
            {
                case SpiralType.Ferma:
                    return new FermaSpiral(layouterSettings.Center, layouterSettings.SpiralCoefficient);
                case SpiralType.InLine:
                    return new InLineSpiral(layouterSettings.Center, layouterSettings.SpiralCoefficient);
                case SpiralType.Rectangle:
                    return new RectangleSpiral(layouterSettings.Center, layouterSettings.SpiralCoefficient);
                default:
                    throw new ArgumentException($"Spiral for  {layouterSettings.SpiralType} does not exist");
            }
        }
    }
}