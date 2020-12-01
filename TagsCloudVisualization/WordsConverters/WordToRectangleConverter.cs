using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class WordsToRectanglesConverter : IWordConverter//TODO Rename
    {
        private readonly ICloudLayout cloudLayout;
        private readonly IWordConfig wordConfig;
        public WordsToRectanglesConverter(ICloudLayout cloudLayout, IWordConfig wordConfig)
        {
            this.cloudLayout = cloudLayout;
            this.wordConfig = wordConfig;
        }
        
        public List<ICloudTag> ConvertWords(List<string> words)
        {
            var result = new List<ICloudTag>();
            var graphics = Graphics.FromHwnd(new IntPtr());
            foreach (var word in words)
            {
                var size = MeasureString(graphics, word, wordConfig.Font);
                var rectangle = cloudLayout.PutNextRectangle(size.ToSize(), word);
                var cloudTag = new CloudTag(rectangle,word);
                result.Add(cloudTag);
            }
            
            return result;
        }

        private static SizeF MeasureString(Graphics graphics, string word, Font font)
        {
            return graphics.MeasureString(word, font);
        }
    }
}