using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class TagCloudVisualization : Visualization
    {
        private List<Tag> tags;

        public TagCloudVisualization(List<Tag> tags)
        {
            this.tags = tags;
            rectangles = tags.Select(tag => tag.Rectangle);
        }

        public override void DrawElements()
        {
            foreach (var tag in tags)
            {
                var shiftedRectangle = ShiftRectangleToCenter(tag.Rectangle);
                graphics.DrawString(tag.Word, tag.Font, new SolidBrush(Color.Black), shiftedRectangle.Location);
            }
        }

    }
}
