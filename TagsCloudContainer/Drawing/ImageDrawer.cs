using System.Drawing;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Drawing
{
    public class ImageDrawer : IDrawer
    {
        public int Width => settings.Size.Width;
        public int Height => settings.Size.Height;

        private readonly WordLayout layout;
        private readonly ImageSettings settings;

        public ImageDrawer(WordLayout layout, ImageSettings settings)
        {
            this.layout = layout;
            this.settings = settings;
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(settings.BackgroundColor), new Rectangle(0, 0, Width, Height));

            foreach (var pair in layout.WordRectangles)
                DrawWord(graphics, pair.Value, pair.Key);
        }

        private void DrawWord(Graphics graphics, Rectangle containingRectangle, string word)
        {
            graphics.DrawString(word, settings.TextFont, new SolidBrush(settings.TextColor), containingRectangle);
        }
    }
}
