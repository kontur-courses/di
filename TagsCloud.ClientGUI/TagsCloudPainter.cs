using System.Drawing;
using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI
{
    public class TagsCloudPainter
    {
        private readonly FontSetting font;
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly SpiralSettings spiralSettings;
        private readonly TagsCreator tagsCreator;

        public TagsCloudPainter(IImageHolder imageHolder, Palette palette,
            TagsCreator tagsCreator, FontSetting font, SpiralSettings spiralSettings)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.tagsCreator = tagsCreator;
            this.font = font;
            this.spiralSettings = spiralSettings;
        }

        public void Paint()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0,
                    imageHolder.GetImageSize().Width, imageHolder.GetImageSize().Height);

                var words = tagsCreator.GetWords();
                var rectangles = tagsCreator.GetRectangles(imageHolder.GetImageSize(), words,
                    spiralSettings.SpiralParameter, font.MainFont.Size);
                for (var i = 0; i < rectangles.Count; ++i)
                {
                    var drawFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    var currentFont = new Font(font.MainFont.FontFamily, (int) (rectangles[i].Height / 1.3),
                        font.MainFont.Style);
                    graphics.DrawRectangle(new Pen(Color.White), rectangles[i]);
                    graphics.DrawString(words[i].Item1, currentFont,
                        new SolidBrush(palette.ForeColor), rectangles[i], drawFormat);
                }
            }

            imageHolder.UpdateUi();
        }
    }
}