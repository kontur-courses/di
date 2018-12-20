using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class FromWordToTagConverter : IConverter<IEnumerable<string>,IEnumerable<Tag>>
    {
        private ImageSettings imageSettings;
        
        public FromWordToTagConverter(ImageSettings imageSettings)
        {
            this.imageSettings = imageSettings;
        }
        
        
        public IEnumerable<Tag> Convert(IEnumerable<string> Data)
        {
            var dict = new Dictionary<string, int>();
            foreach (var word in Data)
            {
                if (!dict.ContainsKey(word))
                    dict[word] = 0;
                dict[word]++;
            }

            var i = 0;

            return dict.OrderBy(x => x.Value).Select(kvp =>
            {
                var font = new Font(FontFamily.GenericSerif, 28 + 3 * i, FontStyle.Italic);
                i++;
                return new Tag
                {
                    Font = font,
                    Size = Graphics.FromImage(new Bitmap(imageSettings.Width,imageSettings.Height)).MeasureString(kvp.Key, font).ToSizeI(),
                    Word = kvp.Key
                };
            }).ToList();
        }

       
    }
}