using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using TagCloud.Drawers;
using TagCloud.ImageSavers;
using TagCloud.Layouters;
using TagCloud.Settings;

namespace TagCloud.Layout
{
    public class TagCloudLayout : ITagCloudLayout
    {
        private LayoutSettings settings;
        private IRectangleLayouter layouter;
        private ITagDrawer tagDrawer;
        private IImageSaver imageSaver;
        
        public TagCloudLayout(LayoutSettings settings,
            IRectangleLayouter layouter, ITagDrawer tagDrawer,
            IImageSaver imageSaver)
        {
            this.settings = settings;
            this.layouter = layouter;
            this.tagDrawer = tagDrawer;
            this.imageSaver = imageSaver;
        }

        public void DrawTagCloud(IReadOnlyCollection<TagInfo> tags)
        {
            Bitmap bitmap = null;
            foreach (var tag in tags.OrderByDescending(t => t.Proportion))
                bitmap = DrawNextTag(tag);
            
            imageSaver.Save(bitmap);
        }

        private Bitmap DrawNextTag(TagInfo tag)
        {
            var font = GetFont(tag);
            var occupiedRectangle = layouter.PutNextRectangle(tagDrawer
                .MeasureStringSize(tag.Value, font));
            return tagDrawer.DrawString(tag.Value, font, occupiedRectangle.Location);
        }
        
        private Font GetFont(TagInfo tag)
        {
            var fontSize = Convert.ToInt32((settings.MaxFontSize - settings.MinFontSize) 
                * tag.Proportion + settings.MinFontSize);
            return new Font(settings.FontFamily, fontSize);
        }
    }
}