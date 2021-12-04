using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider;

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public class RectanglesCloudDrawer : BaseTagsCloudDrawer
    {
        private readonly ITagsCloudDrawerSettingsProvider _drawerSettingsProvider;

        public RectanglesCloudDrawer(ITagsCloudDrawerSettingsProvider drawerSettingsProvider)
        {
            _drawerSettingsProvider = drawerSettingsProvider;
        }

        protected override void FillWithRectangles(Graphics graphics, IEnumerable<Tag> tags, Rectangle bounds)
        {
            using var brush = new SolidBrush(new Color());
            foreach (var tag in tags)
            {
                if (!bounds.Contains(tag.Rectangle))
                    throw new Exception("Image cannot contain all rectangles");
                brush.Color = _drawerSettingsProvider.ColorGenerator.Generate();
                graphics.FillRectangle(brush, tag.Rectangle);
            }
        }
    }
}