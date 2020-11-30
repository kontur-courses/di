using System.Drawing;
using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI
{
    public class TagsCloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly TagsCreator tagsCreator;

        public TagsCloudPainter(IImageHolder imageHolder, Palette palette, TagsCreator tagsCreator)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.tagsCreator = tagsCreator;
        }

        public void Paint()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                var words = tagsCreator.GetWords();
                var rectangles = tagsCreator.GetRectangles(imageHolder.GetImageSize(), words);
                for (var i = 0; i < rectangles.Count; ++i)
                {
                    var drawFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    graphics.DrawRectangle(new Pen(Color.White), rectangles[i]);
                    graphics.DrawString(words[i].Item1, new Font("Arial", (int) (rectangles[i].Height / 1.3)),
                        new SolidBrush(GetRandomColor()), rectangles[i], drawFormat);
                }
            }

            imageHolder.UpdateUi();
        }

        private Color GetRandomColor()
        {
            return palette.PrimaryColor;
        }
    }
}