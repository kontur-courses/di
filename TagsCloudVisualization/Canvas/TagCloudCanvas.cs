using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudVisualization.Canvas
{
    public class TagCloudCanvas : Canvas
    {
        public TagCloudCanvas(int width, int height) : base(width, height) {}

        public override void Draw(Rectangle rectangle, Brush brush = null)
        {
            Graphics.FillRectangle(brush ?? RandomBrush(), rectangle);
        }

        public override void Draw(string word, Font font, RectangleF rectangleF, Brush brush = null)
        {
            Graphics.DrawString(word, font, brush ?? RandomBrush(), rectangleF);
        }

        private Brush RandomBrush()
        {
            var brushes = new List<Brush>()
            {
                new SolidBrush(Color.LawnGreen),
                new SolidBrush(Color.RoyalBlue),
                new SolidBrush(Color.Red),
                new SolidBrush(Color.Orange),
                new SolidBrush(Color.WhiteSmoke),
                new SolidBrush(Color.Aqua),
                new SolidBrush(Color.GreenYellow),
            };

            return brushes[Random.Next(0, brushes.Count)];
        }

        public void Save(string fileName)
        {
            Bitmap.Save(fileName + ".png");
        }

        public override void Save(string directoryPath, string fileName)
        {
            var pathToFile = Path.Combine(directoryPath, fileName + ".png");
            Bitmap.Save(pathToFile, ImageFormat.Png);
        }
    }
}
