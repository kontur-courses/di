using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface
{
    public class TagCloudDrawer : ICloudDrawer
    {
        public void DrawCloudFromPalette(IEnumerable<TextRectangle> rectangles, Point offsetPoint, IImageSettingsProvider drawImageSettingsProvider,
            Palette palette)
        {
            var srcSize = drawImageSettingsProvider.GetImageSize();
            var graphics = drawImageSettingsProvider.StartDrawing();
            var maxOffset = rectangles.MaxBy(x => x.font.Size).font.Size;
            graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), new Rectangle(Point.Empty, srcSize));
            foreach (var textRectangle in rectangles)
            {
                var color = GetColor(palette, (double)textRectangle.font.Size / maxOffset,
                    palette.PrimaryColor.R - palette.SecondaryColor.R,
                    palette.PrimaryColor.G - palette.SecondaryColor.G,
                    palette.PrimaryColor.B - palette.SecondaryColor.B);
                DrawRectangle(offsetPoint, drawImageSettingsProvider, graphics, textRectangle, color, srcSize);
            }
        }

        private static Color GetColor(Palette palette, double multiplier, int rColor, int gColor, int bColor)
        {
            var r = (int)(multiplier * rColor) + palette.SecondaryColor.R;
            var g = (int)(multiplier * gColor) + palette.SecondaryColor.G;
            var b = (int)(multiplier * bColor) + palette.SecondaryColor.B;
            return Color.FromArgb(r, g, b);
        }

        public void DrawCloudRandomColor(IEnumerable<TextRectangle> rectangles, Point offsetPoint,
            IImageSettingsProvider drawImageSettingsProvider)
        {
            var srcSize = drawImageSettingsProvider.GetImageSize();
            var graphics = drawImageSettingsProvider.StartDrawing();
            var random = new Random();
            foreach (var textRectangle in rectangles)
            {
                var color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                DrawRectangle(offsetPoint, drawImageSettingsProvider, graphics, textRectangle, color, srcSize);
            }
        }

        private static void DrawRectangle(Point offsetPoint, IImageSettingsProvider drawImageSettingsProvider,
            Graphics graphics, TextRectangle textRectangle, Color color, Size srcSize)
        {
            graphics.DrawString(textRectangle.text, textRectangle.font, new SolidBrush(color),
                textRectangle.rectangle.Location + new Size(offsetPoint) + new Size(srcSize.Width / 2, srcSize.Height / 2));
            drawImageSettingsProvider.UpdateUi();
        }
    }
}