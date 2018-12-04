using System.Drawing;

namespace TagsCloudVisualization.Visualizing
{
    public class BasicTagPainter : ITagPainter
    {
        private readonly ImageSettings settings;

        public BasicTagPainter(ImageSettings settings)
        {
            this.settings = settings;
        }

        public Brush ChooseBrushForTag(Tag tag)
        {
            return new SolidBrush(settings.TextColor);
        }
    }
}
