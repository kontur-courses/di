using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.App.Layouter;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Layouter
{
    public class TagsLayouter
    {
        private ICircularCloudLayouter cloudLayouter;
        private readonly ITagsReader readTags;
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly FontText fontText;
        public List<TagInfo> Tags;

        public TagsLayouter(ICircularCloudLayouter cloudLayouter, ITagsReader readTags,
            FontText fontText, IImageHolder imageHolder, Palette palette)
        {
            this.cloudLayouter = cloudLayouter;
            this.readTags = readTags;
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.fontText = fontText;
        }

        public void PutAllTags()
        {
            if (readTags.Text is null) return;
            var minT = readTags.Text.Values.Min();
            var maxT = readTags.Text.Values.Max();
            Tags = readTags.Text
                .Select(t => new TagInfo(t.Key, new Font(fontText.Font.FontFamily,
                    CalculateSizeFont(t.Value, minT, maxT, fontText.Font.Size),fontText.Font.Style)))
                .ToList();

            foreach (var tag in Tags)
            {
                var sizeF = CalculateSizeWord(tag.TagText,tag.TagFont);
                var size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
                var rect = cloudLayouter.PutNextRectangle(size);
                tag.TagRect = rect;
            }
            cloudLayouter.Clear();
        }

        private float CalculateSizeFont(int T,int minT, int maxT, float f)
        {
            if (maxT == minT) return f;
            return Math.Max(f * (T - minT) / (maxT - minT) , 1);
        }

        private SizeF CalculateSizeWord(string word, Font font)
        {
            var gph = imageHolder.StartDrawing();
            return gph.MeasureString(word, font);
        }

        public void Paint()
        {
            var imageSize = imageHolder.GetImageSize();
            using (var graphics = imageHolder.StartDrawing())
            using (var backgroundBrush = new SolidBrush(palette.BackgroundColor))
            using (var penBrush = new SolidBrush (palette.PrimaryColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
                if (Tags!=null) 
                    foreach (var tag in Tags)
                        graphics.DrawString(tag.TagText, tag.TagFont, penBrush, tag.TagRect);
            }
            imageHolder.UpdateUi();
        }
    }
}
