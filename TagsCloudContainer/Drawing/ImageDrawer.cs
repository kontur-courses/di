using System.Drawing;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Drawing
{
    public class ImageDrawer : IDrawer
    {
        public  WordLayout Layout { get; }
        public ImageSettings Settings => Layout.Settings;

        public ImageDrawer(WordLayout layout)
        {
            Layout = layout;
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(Settings.BackgroundColor), new Rectangle(new Point(0, 0), Settings.Size));

            foreach (var pair in Layout.WordRectangles)
            {
                var rectangle = pair.Value.Item1;
                var font = Settings.TextFont.SetSize(pair.Value.Item2);
               
                graphics.DrawRectangle(Pens.Blue, rectangle);
                graphics.DrawString(pair.Key, font, new SolidBrush(Settings.TextColor), rectangle);
            }
        }
    }
}
