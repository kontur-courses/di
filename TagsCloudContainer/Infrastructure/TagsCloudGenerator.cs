using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class TagsCloudGenerator : ITagsCloudGenerator
    {
        private readonly IWordLayoutBuilder wordLayoutBuilder;
        private readonly IWordFontSizeProvider fontSizeProvider;

        public TagsCloudGenerator(IWordLayoutBuilder wordLayoutBuilder, IWordFontSizeProvider fontSizeProvider)
        {
            this.wordLayoutBuilder = wordLayoutBuilder;
            this.fontSizeProvider = fontSizeProvider;
        }

        public WordPlate[] GeneratePlates(IEnumerable<string> words, string fontName, PointF center)
        {
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var word in words)
            {
                var font = new Font(fontName, fontSizeProvider.GetFontSize(word));
                wordLayoutBuilder.AddWord(word, graphics.MeasureString(word, font));
            }

            var wordRectangles = wordLayoutBuilder.Build(center);
            return wordRectangles.Select(wr => new WordPlate()
            {
                Font = new Font(fontName, fontSizeProvider.GetFontSize(wr.Word)),
                WordRectangle = wr
            }).ToArray();
        }
    }
}