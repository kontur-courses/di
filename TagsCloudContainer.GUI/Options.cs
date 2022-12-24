using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.GUI
{
    public class Options
    {
        public string InputWordFilename { get; set; }

        public string OutputImageFilename { get; set; }

        public int OutputImageWidth { get; set; } = 500;

        public int OutputImageHeight { get; set; } = 500;

        public string FontFamily { get; set; } = "Consolas";

        public string MinFrequencyColorString { get; set; } = "#FFFFAA00";

        public string MaxFrequencyColorString { get; set; } = "#FFFF0000";

        public float MinFrequencyFontSize { get; set; } = 14F;

        public float MaxFrequencyFontSize { get; set; } = 30F;
    }
}