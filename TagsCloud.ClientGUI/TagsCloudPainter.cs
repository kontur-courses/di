using System.Drawing;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Core;

namespace TagsCloud.ClientGUI
{
    public class TagsCloudPainter
    {
        private readonly FontSetting font;
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly PathSettings pathSettings;
        private readonly SpiralSettings spiralSettings;

        public TagsCloudPainter(IImageHolder imageHolder, Palette palette,
            FontSetting font, SpiralSettings spiralSettings, PathSettings pathSettings)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.font = font;
            this.spiralSettings = spiralSettings;
            this.pathSettings = pathSettings;
        }

        public void Paint()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                var imageSize = imageHolder.GetImageSize();
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0,
                    imageSize.Width, imageSize.Height);

                var words = TagsHelper.GetWords(pathSettings.PathToText, pathSettings.PathToBoringWords,
                    pathSettings.PathToDictionary, pathSettings.PathToAffix);
                var rectangles = TagsHelper.GetRectangles(imageSize, words,
                    spiralSettings.SpiralParameter, font.MainFont.Size);

                for (var i = 0; i < rectangles.Count; ++i)
                {
                    var drawFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    graphics.DrawRectangle(new Pen(Color.White), rectangles[i]);

                    using (var currentFont = new Font(font.MainFont.FontFamily, (int) (rectangles[i].Height / 1.3),
                        font.MainFont.Style))
                    {
                        graphics.DrawString(words[i].Item1, currentFont,
                            new SolidBrush(palette.ForeColor), rectangles[i], drawFormat);
                        currentFont.Dispose();
                    }
                }
            }

            imageHolder.UpdateUi();
        }
    }
}