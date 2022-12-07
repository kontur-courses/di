using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    internal class ProcessingSettings
    {
        private const int defaultMinWordLength = 2;
        private const int defaultMaxWordLength = 30;
        public int MinWordLength { get; set; }
        public int MaxWordLength { get; set; }
        public string[] ExcludedWords { get; set; }
        //public Speech.PartsOfSpeech[] ExcludedPartsOfSpeech { get; set; }

        public ProcessingSettings(
            //Speech.PartsOfSpeech[] excludedPartsOfSpeech = new,
            string[] excludedWords = null,
            int minLength = defaultMinWordLength,
            int maxLength = defaultMaxWordLength)
        {
            //ExcludedPartsOfSpeech = excludedPartsOfSpeech;
            ExcludedWords = excludedWords != null ? excludedWords : new string[] { };
            MinWordLength = minLength;
            MaxWordLength = maxLength;
        }
    }
}
