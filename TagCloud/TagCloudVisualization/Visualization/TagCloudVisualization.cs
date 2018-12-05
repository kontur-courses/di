using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Forms;
using TagCloud.Settings;
using TagCloud.TagCloudVisualization.Analyzer;

namespace TagCloud.TagCloudVisualization.Visualization
{
    public class TagCloudVisualizer : Visualization
    {
        private readonly List<Tag> tags;

        public TagCloudVisualizer(ImageBox imageBox, ImageSettings imageSettings, List<Tag> tags)
        {
            ImageBox = imageBox;
            ImageSettings = imageSettings;
            this.tags = tags;
            Rectangles = tags.Select(tag => tag.Rectangle);
        }

        protected override void DrawElements()
        {
            foreach (var tag in tags)
            {
                var shiftedRectangle = ShiftRectangleToCenter(tag.Rectangle);
                Graphics.DrawString(tag.Word, tag.Font, new SolidBrush(ImageSettings.TextColor), shiftedRectangle.Location);
            }
        }

    }
}
