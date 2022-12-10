using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.Settings
{
    public class WordFontSizeSettings
    {
        public float MinFrequencyFontSize { get; set; }
        public float MaxFrequencyFontSize { get; set; }
        public Dictionary<string, int> WordFrequencies { get; set; }
    }
}