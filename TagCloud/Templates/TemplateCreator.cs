using System.Collections.Generic;
using System.Drawing;
using TagCloud.CloudLayouter;
using TagCloud.Templates.Colors;

namespace TagCloud.Templates
{
    public class TemplateCreator : ITemplateCreator
    {
        private readonly FontFamily fontFamily;
        private readonly Color backgroundColor;
        private readonly Size size;
        private readonly IFontSizeCalculator fontSizeCalculator;
        private readonly IColorGenerator colorGenerator;
        private readonly ICloudLayouter cloudLayouter;

        public TemplateCreator(FontFamily fontFamily, Color backgroundColor, Size size, 
            IFontSizeCalculator fontSizeCalculator, IColorGenerator colorGenerator, ICloudLayouter cloudLayouter)
        {
            this.fontFamily = fontFamily;
            this.backgroundColor = backgroundColor;
            this.size = size;
            this.fontSizeCalculator = fontSizeCalculator;
            this.colorGenerator = colorGenerator;
            this.cloudLayouter = cloudLayouter;
        }

        public ITemplate GetTemplate(IEnumerable<string> words)
        {
            var template = new Template();
            template.Size = size;
            var wordToSize = fontSizeCalculator.GetFontSizes(words);
            var bitmap = new Bitmap(1, 1);
            var g = Graphics.FromImage(bitmap);
            foreach (var w in wordToSize)
            {
                var font = new Font(fontFamily, w.Value);
                var wordParameter = new WordParameter(w.Key, font, colorGenerator.GetColor(w.Key));
                var wordSize = g.MeasureString(w.Key, font);
                wordParameter.WordRectangleF = cloudLayouter.PutNextRectangle(wordSize);
                template.Add(wordParameter);
            }

            template.BackgroundColor = backgroundColor;
            return template;
        }
    }
}