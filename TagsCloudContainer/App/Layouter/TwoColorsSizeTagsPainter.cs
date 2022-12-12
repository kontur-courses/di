using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Layouter
{
    class TwoColorsSizeTagsPainter : ITagsPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;

        public TwoColorsSizeTagsPainter(IImageHolder imageHolder, Palette palette)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
        }

        public bool CanPaint(PainterType painterType)
        {
            return painterType == PainterType.TwoColorDependOnSize;
        }

        public void Paint(List<TagInfo> tags)
        {
            var imageSize = imageHolder.GetImageSize();
            using (var graphics = imageHolder.StartDrawing())
            using (var backgroundBrush = new SolidBrush(palette.BackgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
                if (tags != null)
                {
                    var middleSize = tags.Average(t => t.TagFont.Size);

                    foreach (var tag in tags)
                    {
                        using var penBrush = new SolidBrush(tag.TagFont.Size >= middleSize ? palette.PrimaryColor : palette.SecondaryColor);
                        graphics.DrawString(tag.TagText, tag.TagFont, penBrush, tag.TagRect);
                    }
                }
            }
            imageHolder.UpdateUi();
        }
    }
}
