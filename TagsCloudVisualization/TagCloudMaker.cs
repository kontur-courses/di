using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloudMaker
    {
        private readonly ICloudMaker cloudMaker; 
        private readonly ITokenColorChooser tokenColorChooser;
        private readonly Graphics graphics = Graphics.FromImage(new Bitmap(1,1));

        public TagCloudMaker(ICloudMaker cloudMaker, ITokenColorChooser tokenColorChooser)
        {
            this.cloudMaker = cloudMaker;
            this.tokenColorChooser = tokenColorChooser;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }

        public Tag[] CreateTagCloud(Token[] tokens, Font font)
        {
            var tags = new List<Tag>();
            foreach (var token in tokens)
            {
                var size = GetSize(token, font);
                var rect = cloudMaker.PutRectangle(size.tagSize);
                var color = tokenColorChooser.GetTokenColor(token);
                tags.Add(new Tag(token.Value, font, color, rect, size.FontSize));
            }
            return tags.ToArray(); 
        }

        private (SizeF tagSize, SizeF FontSize) GetSize(Token token, Font font)
        {
            var fontSize = graphics.MeasureString(token.Value, font);
            var scale = (float)Math.Sqrt(token.Weight / (fontSize.Height * fontSize.Width));
            return (new SizeF(fontSize.Width * scale,
                fontSize.Height * scale), fontSize); 
        }
    }
}