using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Layouter
{
    public class TagsPainter : ITagsPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;

        public TagsPainter(IImageHolder imageHolder, Palette palette)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
        }

        public bool CanPaint(PainterType painterType)
        {
            return painterType == PainterType.OneColor;
        }

        public void Paint(List<TagInfo> tags)
        {
            var imageSize = imageHolder.GetImageSize();
            using (var graphics = imageHolder.StartDrawing())
            using (var backgroundBrush = new SolidBrush(palette.BackgroundColor))
            using (var penBrush = new SolidBrush(palette.PrimaryColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
                if (tags != null)
                    foreach (var tag in tags)
                        graphics.DrawString(tag.TagText, tag.TagFont, penBrush, tag.TagRect);
            }
            imageHolder.UpdateUi();
        }
    }
}
