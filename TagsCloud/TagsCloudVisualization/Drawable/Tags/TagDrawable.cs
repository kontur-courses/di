using System.Drawing;
using TagsCloudDrawer;
using TagsCloudVisualization.Drawable.Tags.Settings;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Drawable.Tags
{
    public class TagDrawable : IDrawable
    {
        private readonly Tag _tag;
        private readonly ITagDrawableSettingsProvider _settingsProvider;

        public TagDrawable(Tag tag, Rectangle bounds, ITagDrawableSettingsProvider settingsProvider)
        {
            Bounds = bounds;
            _tag = tag;
            _settingsProvider = settingsProvider;
        }

        public void Draw(Graphics graphics)
        {
            using var brush = new SolidBrush(_settingsProvider.ColorGenerator.Generate(_tag));
            var fontName = _settingsProvider.Font.Family;
            var fontSize = _tag.Weight * _settingsProvider.Font.MaxSize;
            using var font = new Font(fontName, fontSize);
            DrawStringInside(graphics, Bounds, font, brush, _tag.Word);
        }

        private static void DrawStringInside(Graphics graphics, Rectangle rect, Font font, Brush brush, string text)
        {
            var textSize = graphics.MeasureString(text, font);
            var state = graphics.Save();
            graphics.TranslateTransform(rect.Left, rect.Top);
            graphics.ScaleTransform(rect.Width / textSize.Width, rect.Height / textSize.Height);
            graphics.DrawString(text, font, brush, PointF.Empty);
            graphics.Restore(state);
        }

        public Rectangle Bounds { get; }

        public IDrawable Shift(Size vector) => new TagDrawable(_tag, Bounds.Shift(vector), _settingsProvider);
    }
}