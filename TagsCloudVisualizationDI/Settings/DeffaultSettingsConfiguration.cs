using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagsCloudVisualizationDI.Settings
{
    public class DeffaultSettingsConfiguration : ISettingsConfiguration
    {
        public string SavePath
        {
            get => "C:/GitHub/di/TagsCloudVisualizationDI/img_words";
        }

        public SolidBrush Brush => new SolidBrush(Color.Black);


        public ImageFormat Format => ImageFormat.Png;

        public List<string> ExcludedWords
        {
            get => new List<string> {"к", "с", "он", "она", "в", "как", "что"};
        }
    }
}
