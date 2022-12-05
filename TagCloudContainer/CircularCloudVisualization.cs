using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;


namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public Dictionary<string, int> sizeDictionary;
         
        Graphics graphics;
        public CircularCloudLayouter(Dictionary<string, int> sizeDictionary)
        {
            this.sizeDictionary = sizeDictionary;
            graphics= Graphics.FromImage(new Bitmap(1,1));
        }

        public IEnumerable<Tuple<string, Size, Font>> GetNextRectangleOptions()
        {
            foreach (var pair in sizeDictionary)
            {
                var font = new Font("Times", pair.Value);
                var size = graphics.MeasureString(pair.Key, font).ToSize();
                yield return Tuple.Create(pair.Key, size, font);
            }
        }

    }

}