using System.Drawing;

namespace TagsCloudVisualization
{
    public class LinearSizer : ISizeDefiner
    {
        private readonly int shift;

        public LinearSizer()
        {
            shift = 8;
        }

        public LinearSizer(int shift)
        {
            this.shift = shift;
        }

        public Size GetSize(GraphicWord graphicWord)
        {
            return new Size(graphicWord.Value.Length * (graphicWord.Rate + shift), graphicWord.Rate + shift);
        }
    }
}
