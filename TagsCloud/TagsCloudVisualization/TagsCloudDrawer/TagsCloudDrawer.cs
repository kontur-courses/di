using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider;

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public class TagsCloudDrawer : BaseTagsCloudDrawer
    {
        private readonly ITagsCloudDrawerSettingsProvider _drawerSettingsProvider;

        public TagsCloudDrawer(ITagsCloudDrawerSettingsProvider drawerSettingsProvider)
        {
            _drawerSettingsProvider =
                drawerSettingsProvider ?? throw new ArgumentNullException(nameof(drawerSettingsProvider));
        }

        protected override void FillWithRectangles(Graphics graphics, IEnumerable<Tag> tags, Rectangle bounds)
        {
            using var brush = new SolidBrush(new Color());
            using var pen = new Pen(brush);
            foreach (var tag in tags)
            {
                if (!bounds.Contains(tag.Rectangle))
                    throw new Exception("Image cannot contain all rectangles");
                brush.Color = _drawerSettingsProvider.ColorGenerator.Generate();
                graphics.DrawString(tag.Word, _drawerSettingsProvider.Font, brush, tag.Rectangle);
            }
        }
    }
}