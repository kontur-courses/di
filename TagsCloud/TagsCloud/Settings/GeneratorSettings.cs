using System.Drawing;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Settings
{
    public class GeneratorSettings : ISettings
    {
        public GeneratorSettings(IPainterSettings painterSettings, IFactorySettings factorySettings)
        {
            PainterSettings = painterSettings;
            FactorySettings = factorySettings;
            Reset();
        }

        public string Font { get; set; }
        public Size? ImageSize { get; set; }
        public IPainterSettings PainterSettings { get; set; }
        public IFactorySettings FactorySettings { get; set; }

        public virtual void Reset()
        {
            Font = "Arial";
            ImageSize = null;
            PainterSettings.Reset();
            FactorySettings.Reset();
        }
    }
}