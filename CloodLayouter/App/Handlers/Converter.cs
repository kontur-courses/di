using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class FromWordToTagConverter : IConverter<IEnumerable<string>, IEnumerable<Tag>>
    {
        private readonly ImageSettings imageSettings;

        public FromWordToTagConverter(ImageSettings imageSettings)
        {
            this.imageSettings = imageSettings;
        }


        public IEnumerable<Tag> Convert(IEnumerable<string> data)
        {
            var i = 0;
            return data.GroupBy(word => word)
                .OrderByDescending(g => g.Count())
                .Reverse()
                .Select(kvp =>
                {
                    var font = new Font(FontFamily.GenericSerif, 28 + 3 * i, FontStyle.Italic);
                    i++;
                    return new Tag
                    {
                        Font = font,
                        Size = Graphics.FromImage(new Bitmap(imageSettings.Width, imageSettings.Height))
                            .MeasureString(kvp.Key, font).ToSizeI(),
                        Word = kvp.Key
                    };
                });
        }
    }
}