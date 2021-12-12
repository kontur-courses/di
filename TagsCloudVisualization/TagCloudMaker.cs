using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

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

        public Tag[] CreateTagCloud(string text, Font font)
        {
            var tokens = tokenGenerator.GetTokens(text);
            var tags = new List<Tag>();
            foreach (var token in tokens)
            {
                var (size, curFont) = GetSize(token, font);
                var rect = cloudMaker.PutRectangle(size);
                var color = tokenColorChooser.GetTokenColor(token);
                tags.Add(new Tag(token.Value, curFont, color, rect));
            }
            return tags.ToArray(); 
        }

        private (Size, Font) GetSize(Token token, Font font)
        {
            var scale = (float)Math.Sqrt(token.Weight);
            var curFont = new Font(font.FontFamily, font.Size * scale, font.Style);
            var fontSize = graphics.MeasureString(token.Value, curFont, PointF.Empty, StringFormat.GenericTypographic);
            return (new Size((int)Math.Ceiling(fontSize.Width ),
                (int)Math.Round(fontSize.Height)), curFont); 
        }
    }
}