using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    class Program
    {
        private static readonly Dictionary<string, int> wordsDictionary = new Dictionary<string, int>()
        {
            {"Participation", 90},
            {"Web 2.0", 90 },
            {"Java", 70 },
            {"Usability", 50},
            {"Design", 40},
            {"Standartization", 30},
            {"Python", 20},
            {"Null", 20},
            { "Hello world", 20}
       
        };
        static void Main(string[] args)
        {
            var cloudCenter = new Point(400, 400);
            var layout = new CircularCloudLayouter(cloudCenter);
            var tagsDict = new Dictionary<Rectangle, (string, Font)>();

            foreach (var word in wordsDictionary)
            {
                var font = new Font(new FontFamily("Tahoma"), word.Value, FontStyle.Regular, GraphicsUnit.Pixel);
                var tagSize = TextRenderer.MeasureText(word.Key,font);
                tagsDict.Add(layout.PutNextRectangle(tagSize), (word.Key, font));
//                rectangleList.Add(layout.PutNextRectangle(tagSize));
            }

            CloudTagDrawer.DrawTagsToFile(cloudCenter, tagsDict, "1.bmp", 800, 800);
            CloudTagDrawer.DrawTagsToForm(cloudCenter, tagsDict ,800, 800);

        }
    }
}
