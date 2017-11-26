using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class TagHandler
    {
        public Dictionary<Rectangle, (string, Font)> MakeTagRectangles(Dictionary<string, int> frequencyDict, Point cloudCenter,
            ICloudLayouter layout)
        {
            
            var tagsDict = new Dictionary<Rectangle, (string, Font)>();

            var maxfreq = frequencyDict.Values.Max();            
            var fontSize = new FontSize(10,80);
            
            foreach (var word in frequencyDict)
            {
                var font = new Font(new FontFamily("Tahoma"), fontSize.GetFontSizeByFreq(maxfreq, word.Value), FontStyle.Regular, GraphicsUnit.Pixel);
                var tagSize = TextRenderer.MeasureText(word.Key,font);
                tagsDict.Add(layout.PutNextRectangle(tagSize), (word.Key, font));
            }

            return tagsDict;
        }
    }
}