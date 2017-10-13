using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudContainer
{
    class TagsCloudVisualizator : ITagsCloudVisualizator
    {
        private readonly ITagsCloudContainer tagsCloudContainer;
        private readonly Bitmap image;
        private IColorPicker rectangleColorPicker;
        private IColorPicker textColorPicker;

        public Font TextFont { get; set; }

        public TagsCloudVisualizator(ITagsCloudContainer tagsCloudContainer, IColorPicker[] colorPickers, Point center, Font textFont)
        {
            this.tagsCloudContainer = tagsCloudContainer;
            this.rectangleColorPicker = colorPickers[0];
            this.textColorPicker = colorPickers[1];
            this.image = new Bitmap(center.X * 2, center.Y * 2);
            TextFont = textFont;
        }

        public Bitmap GetTagsCloudImage()
        {
            using (var canvas = Graphics.FromImage(image))
            {
                canvas.FillRectangle(Brushes.White, new Rectangle(0, 0, image.Width, image.Height));

                foreach (var tagRectangle in tagsCloudContainer.GetTagsRectangleData())
                {
                    canvas.FillRectangle(rectangleColorPicker.GenerateColor(), tagRectangle.Value);
                    canvas.DrawString(tagRectangle.Key, TextFont, textColorPicker.GenerateColor(), tagRectangle.Value);
                }
            }

            return image;
        }
    }
}
