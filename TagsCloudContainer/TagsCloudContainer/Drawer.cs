using System.Collections.Generic;
using System.Drawing;


namespace TagsCloudContainer
{
    internal class Drawer: IDrawer<Word>
    {
        private readonly DrawSettings<Word> _drawSettings;
        private readonly IEnumerable<ItemToDraw<Word>> _itemsToDraws;
        public Drawer(DrawSettings<Word> drawSettings, WordLayouter wordLayouter)
        {
            _drawSettings = drawSettings;
            _itemsToDraws = wordLayouter.GetItemsToDraws();
        }

        public void DrawItems()
        {
            var bitmap = new Bitmap(_drawSettings.GetImageSize().Width, _drawSettings.GetImageSize().Height);
            var g = Graphics.FromImage(bitmap);

            foreach (var item in _itemsToDraws)
            {
                var size = item.Height;
                var font = new Font(new FontFamily(_drawSettings.GetFontName()), size);

                g.DrawString(
                    item.Body.Value,
                    font,
                    _drawSettings.GetBrush(item),
                    item.X, item.Y);
            }

            bitmap.Save(_drawSettings.GetFullFileName());

            g.Dispose();
            bitmap.Dispose();
        }
    }
}