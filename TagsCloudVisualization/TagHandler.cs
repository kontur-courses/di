using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class TagHandler : ITagHandler
    {
        private readonly ICloudLayouter layouter;
        private readonly string fontFamily;

        public TagHandler(ICloudLayouter layouter, string fontFamily)
        {
            this.layouter = layouter;
            this.fontFamily = fontFamily;
        }
        
        public Dictionary<Rectangle, (string, Font)> MakeTagRectangles(
            Dictionary<string, int> frequencyDict)
        {
            
            var tagsDict = new Dictionary<Rectangle, (string, Font)>();

            var maxfreq = frequencyDict.Values.Max();            
            var fontSize = new FontSizeMaker(10,80);
            
            foreach (var word in frequencyDict)
            {
                var font = new Font(new FontFamily(fontFamily), fontSize.GetFontSizeByFreq(maxfreq, word.Value), FontStyle.Regular, GraphicsUnit.Pixel);
                var tagSize = TextRenderer.MeasureText(word.Key,font);
                tagsDict.Add(layouter.PutNextRectangle(tagSize), (word.Key, font));
            }
            return tagsDict;
        }
    }
}