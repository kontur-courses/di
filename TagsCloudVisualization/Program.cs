using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("book1.txt");
            var frequencyDict = new FileParser(150, 4).GetWordsFrequensy(input);
        
            
            var cloudCenter = new Point(400, 400);
            var layout = new CircularCloudLayouter(cloudCenter);
            var tagsDict = new Dictionary<Rectangle, (string, Font)>();

            var maxfreq = frequencyDict.Values.Max();            
            var fontSize = new FontSize(10,80);
            
            foreach (var word in frequencyDict)
            {
                var font = new Font(new FontFamily("Tahoma"), fontSize.GetFontSizeByFreq(maxfreq, word.Value), FontStyle.Regular, GraphicsUnit.Pixel);
                var tagSize = TextRenderer.MeasureText(word.Key,font);
                tagsDict.Add(layout.PutNextRectangle(tagSize), (word.Key, font));
            }

            CloudTagDrawer.DrawTagsToFile(cloudCenter, tagsDict, "1.bmp", 800, 800);
            CloudTagDrawer.DrawTagsToForm(cloudCenter, tagsDict ,800, 800);

        }
    }
}
