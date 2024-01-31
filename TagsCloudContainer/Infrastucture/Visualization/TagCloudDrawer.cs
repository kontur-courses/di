using TagsCloudContainer.Infrastucture.Extensions;
using TagsCloudContainer.Infrastucture.Settings;

namespace TagsCloudContainer.Infrastucture.Visualization
{
    public class TagCloudDrawer : IDrawer
    {
        public void Draw(List<TextRectangle> rectangles, PictureBox pictureBox, ImageSettings imageSettings)
        {
            using (var graphics = pictureBox.StartDrawing())
            {
                graphics.Clear(imageSettings.BackgroundColor);

                using var textBrush = new SolidBrush(imageSettings.TextColor);
                using var rectBorderPen = new Pen(imageSettings.RectangleBordersColor);
                using var rectBackgroundBrush = new SolidBrush(imageSettings.RectangleBackgroundColor);

                foreach (var rect in rectangles)
                {
                    graphics.FillRectangle(rectBackgroundBrush, rect.Rectangle);
                    graphics.DrawRectangle(rectBorderPen, rect.Rectangle);
                    graphics.DrawString(rect.Text, rect.Font, textBrush, rect.Rectangle);
                }
            }

            pictureBox.UpdateUi();
        }
    }
}