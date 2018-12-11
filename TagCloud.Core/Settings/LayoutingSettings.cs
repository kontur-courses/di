using System.Drawing;

namespace TagCloud.Core.Settings
{
    public class LayoutingSettings : ISettings
    {
        public double SpiralStepMultiplier { get; set; } = 1e-2;

        public string GetSettingsName()
        {
            return "Layouting settings";
        }
    }
}