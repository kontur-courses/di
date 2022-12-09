using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer;

namespace TagsCloudWinformsApp
{
    internal class SettingsHandler : ISettingsProvider
    {
        public Settings Settings =>
            new()
            {
                BackgroundColor = LocalSettings.BackgroundColor,
                FontColor = LocalSettings.FontColor,
                Font = LocalSettings.Font,
                FrequencyRatio = LocalSettings.FrequencyRatio,
                ImageSize = LocalSettings.ImageSize,
                Layouter = LocalSettings.Layouter
            };


        public Settings LocalSettings = new()
        {
            BackgroundColor = Color.Black,
            FontColor = Color.Cyan,
            Font = new Font(FontFamily.GenericSerif, 26),
            FrequencyRatio = 1.2f,
            ImageSize = new Size(1000, 1000),
            Layouter = LayouterType.Spiral
        };
    }
}