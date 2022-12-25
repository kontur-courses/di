using System.Linq;
using TagCloudContainer.Models;
using TagCloudGUI.Interfaces;

namespace TagCloudGUI
{
    public class TagCloudDrawer : ICloudDrawer
    {
        public void DrawCloudFromPalette(
            IEnumerable<RectangleWithText> rectangles, 
            IImageSettingsProvider drawImageSettingsProvider,
            Palette palette)
        {

            var srcSize = drawImageSettingsProvider.GetImageSize();
            var graphics = drawImageSettingsProvider.StartDrawing();
            var maxFontSize = rectangles.MaxBy(x => x.Font.Size).Font.Size;
            var minFontSize = rectangles.MinBy(x => x.Font.Size).Font.Size;

            var diff = maxFontSize != minFontSize ? maxFontSize - minFontSize : 1;

            graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), new Rectangle(Point.Empty, srcSize));
            
            foreach (var textRectangle in rectangles)
            {
                var coef = ((double)textRectangle.Font.Size - minFontSize) / diff;

                var color = Color.FromArgb(
                    (int)((palette.SecondaryColor.R * (1 - coef)) + (palette.PrimaryColor.R * (coef))),
                    (int)((palette.SecondaryColor.G * (1 - coef)) + (palette.PrimaryColor.G * (coef))),
                    (int)((palette.SecondaryColor.B * (1 - coef)) + (palette.PrimaryColor.B * (coef)))
                );

                DrawTag(drawImageSettingsProvider, graphics, textRectangle, color, srcSize);
            }
        }

        private static void DrawTag(IImageSettingsProvider drawImageSettingsProvider,
            Graphics graphics, RectangleWithText textRectangle, Color color, Size srcSize)
        {
            graphics.DrawString(textRectangle.Text, textRectangle.Font, new SolidBrush(color),
                textRectangle.Rectangle.Location + new Size(srcSize.Width / 2, srcSize.Height / 2));
            drawImageSettingsProvider.UpdateUi();
        }
    }
}