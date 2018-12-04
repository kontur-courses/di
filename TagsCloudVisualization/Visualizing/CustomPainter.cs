using System.Drawing;

namespace TagsCloudVisualization.Visualizing
{
    public class CustomPainter : ITagPainter
    {
        private const int LengthThreshold = 7;

        public Brush ChooseBrushForTag(Tag tag)
        {
            if (tag.Word.Length > LengthThreshold)
                return new SolidBrush(Color.Red);
            else 
                return new SolidBrush(Color.Azure);
        }
    }
}
