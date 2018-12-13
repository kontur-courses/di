using TagCloud.Core.Settings.Interfaces;

namespace TagCloud.Core.Settings.DefaultImplementations
{
    public class LayoutingSettings : ILayoutingSettings
    {
        public double SpiralStepMultiplier { get; set; } = 1e-2;
    }
}