using System.Drawing;
using TagsCloudContainer.Common;
using TagsCloudContainer.TextAnalyzing;

namespace TagsCloudContainer
{
    public class Painter
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly TagCreator tagCreator;

        public Painter(Palette palette, IImageHolder imageHolder, TagCreator tagCreator)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.tagCreator = tagCreator;
        }

        public void PaintTag(Tag tag, Graphics graphics)
        {
            graphics.DrawString(tag.Text, new Font("Times New Roman", tag.FontSize), new SolidBrush(palette.TextColor),
                tag.Rectangle);
            graphics.Save();
        }

        public void PaintTags()
        {
            var tags = tagCreator.GetTagsForVisualization();
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.Clear(palette.BackgroundColor);
                foreach (var tag in tags) PaintTag(tag, graphics);
            }

            imageHolder.UpdateUi();
        }
    }
}