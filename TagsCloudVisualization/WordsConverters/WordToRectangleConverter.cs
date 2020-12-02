using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class WordsToRectanglesConverter : IWordConverter//TODO Rename
    {
        private readonly ICloudLayout cloudLayout;
        private readonly IConfig config;
        public WordsToRectanglesConverter(ICloudLayout cloudLayout, IConfig config)
        {
            this.cloudLayout = cloudLayout;
            this.config = config;
        }
        
        public List<ICloudTag> ConvertWords(List<string> words)
        {
            var result = new List<ICloudTag>();
            var graphics = Graphics.FromHwnd(new IntPtr());
            foreach (var word in words)
            {
                var size = MeasureString(graphics, word, config.Font);
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