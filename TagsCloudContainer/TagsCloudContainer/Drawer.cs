using System.Collections.Generic;
using System.Drawing;


namespace TagsCloudContainer
{
    internal class Drawer: IDrawer<Word>
    {
        private readonly DrawSettings<Word> _drawSettings;
        public Drawer(DrawSettings<Word> drawSettings)
        {
            _drawSettings = drawSettings;
        }

        public void DrawItems(IEnumerable<ItemToDraw<Word>> itemsToDraws)
        {
            var bitmap = new Bitmap(_drawSettings.GetImageSize().Width, _drawSettings.GetImageSize().Height);
            var g = Graphics.FromImage(bitmap);

            foreach (var item in itemsToDraws)
            {
                var size = item.Height;
                var font = new Font(new FontFamily(_drawSettings.GetFontName()), size);

                g.DrawString(
                    item.Body.Value,
                    font,
                    _drawSettings.GetBrush(item),
                    item.X, item.Y);
            }

            bitmap.Save(_drawSettings.GetFileFullName());

            g.Dispose();
            bitmap.Dispose();
        }
    }
}