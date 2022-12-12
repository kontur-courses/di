using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Layouter
{
    class TwoColorsTagsPainter : ITagsPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;

        public TwoColorsTagsPainter(IImageHolder imageHolder, Palette palette)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
        }

        public bool CanPaint(PainterType painterType)
        {
            return painterType == PainterType.TwoColor;
        }

        public void Paint(List<TagInfo> tags)
        {
            var imageSize = imageHolder.GetImageSize();
            using (var graphics = imageHolder.StartDrawing())
            using (var backgroundBrush = new SolidBrush(palette.BackgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
                var i = 0;
                if (tags != null)
                    foreach (var tag in tags)
                    {
                        using (var penBrush = new SolidBrush(i%2 == 0 ? palette.PrimaryColor : palette.SecondaryColor))
                            graphics.DrawString(tag.TagText, tag.TagFont, penBrush, tag.TagRect);
                        i++;
                    }
            }
            imageHolder.UpdateUi();
        }
    }
}
