using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.Settings
{
    public class WordColorSettings
    {
        public Color MinFrequencyColor { get; set; }
        public Color MaxFrequencyColor { get; set; }
        public Dictionary<string, int> WordFrequencies { get; set; }
    }
}