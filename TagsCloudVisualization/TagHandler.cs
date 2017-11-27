using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class TagHandler : ITagHandler
    {
        private readonly ICloudLayouter layouter;
        private readonly IFontSizeMaker fontSizeMaker;
        private readonly string fontFamily;

        public TagHandler(ICloudLayouter layouter,IFontSizeMaker fontSizeMaker, string fontFamily)
        {
            this.layouter = layouter;
            this.fontSizeMaker = fontSizeMaker;
            this.fontFamily = fontFamily;
        }
        
        public Dictionary<Rectangle, (string, Font)> MakeTagRectangles(
            Dictionary<string, int> frequencyDict)
        {
            
            var tagsDict = new Dictionary<Rectangle, (string, Font)>();
            var maxfreq = frequencyDict.Values.Max();            
            
            foreach (var word in frequencyDict)
            {
                var font = new Font(new FontFamily(fontFamily), fontSizeMaker.GetFontSizeByFreq(maxfreq, word.Value), FontStyle.Regular, GraphicsUnit.Pixel);
                var tagSize = TextRenderer.MeasureText(word.Key,font);
                tagsDict.Add(layouter.PutNextRectangle(tagSize), (word.Key, font));
            }
            return tagsDict;
        }
    }
}