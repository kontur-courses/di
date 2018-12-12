using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core.Settings;
using TagCloud.Core.Util;

namespace TagCloud.Core.Painters
{
    public class OneColorPainter : IPainter
    {
        private readonly PaintingSettings settings;

        public OneColorPainter(PaintingSettings settings)
        {
            this.settings = settings;
        }

        public void PaintTags(IEnumerable<Tag> tags)
        {
            if (tags == null)
                throw new ArgumentNullException(nameof(tags));
            foreach (var tag in tags)
                tag.Brush = settings.TagBrush;
        }

        public void SetBackgroundColorFor(Graphics graphics)
        {
            graphics.Clear(settings.BackgroundColor);
        }
    }
}