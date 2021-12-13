using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloudMaker
    {
        private readonly TokenGenerator tokenGenerator;
        private readonly ICloudMaker cloudMaker; 
        private readonly ITokenColorChooser tokenColorChooser;
        private readonly Graphics graphics = Graphics.FromImage(new Bitmap(1,1));

        public TagCloudMaker(ICloudMaker cloudMaker, ITokenColorChooser tokenColorChooser, 
            TokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
            this.cloudMaker = cloudMaker;
            this.tokenColorChooser = tokenColorChooser;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        }

        public Tag[] CreateTagCloud(string text, Font font, int tagCount)
        {
            var tokens = tokenGenerator.GetTokens(text);
            var tags = new List<Tag>();
            foreach (var token in tokens.Take(tagCount))
            {
                var size = GetSize(token, font);
                var rect = cloudMaker.PutRectangle(size);
                var color = tokenColorChooser.GetTokenColor(token);
                tags.Add(new Tag(token.Value, font, color, rect));
            }
            return tags.ToArray(); 
        }

        private Size GetSize(Token token, Font font)
        {
            var fontSize = graphics.MeasureString(token.Value, font);
            var scale = Math.Sqrt(token.Weight);
            return (new Size((int)Math.Ceiling(fontSize.Width * scale),
                (int)Math.Ceiling(fontSize.Height * scale))); 
        }
    }
}