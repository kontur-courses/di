using TagCloudGraphicalUserInterface;
using TagCloudGraphicalUserInterface.Interfaces;

namespace TagsCloudVisualization
{
    public class TagCloudDrawer : ICloudDrawer
    {
        public void DrawCloud(IEnumerable<TextRectangle> rectangles, Point offsetPoint, IImageSettingsProvider drawImageSettingsProvider,
            Palette palette)
        {
            var srcSize = drawImageSettingsProvider.GetImageSize();
            var graphics = drawImageSettingsProvider.StartDrawing();

            var maxOffset = rectangles.MaxBy(x => x.font.Size).font.Size;
            graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), new Rectangle(Point.Empty, srcSize));
            var minOffset = rectangles.MinBy(x => x.font.Size).font.Size;
            foreach (var textRectangle in rectangles)
            {
                var multiplier = (double)textRectangle.font.Size / maxOffset;
                var color = GetColor(palette, multiplier,
                    palette.PrimaryColor.R - palette.SecondaryColor.R,
                    palette.PrimaryColor.G - palette.SecondaryColor.G,
                    palette.PrimaryColor.B - palette.SecondaryColor.B);
                graphics.DrawString(textRectangle.text, textRectangle.font, new SolidBrush(color),
                    textRectangle.rectangle.Location + new Size(offsetPoint) + new Size(srcSize.Width / 2, srcSize.Height / 2));
                drawImageSettingsProvider.UpdateUi();
            }
        }

        private static Color GetColor(Palette palette, double multiplier, int rColor, int gColor, int bColor)
        {
            var r = (int)(multiplier * rColor) + palette.SecondaryColor.R;
            var g = (int)(multiplier * gColor) + palette.SecondaryColor.G;
            var b = (int)(multiplier * bColor) + palette.SecondaryColor.B;
            return Color.FromArgb(r, g, b);
        }
    }
}