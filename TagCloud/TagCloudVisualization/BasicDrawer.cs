using System.Drawing;

namespace TagCloudVisualization
{
    public class BasicDrawer : IWordDrawer
    {
        public void DrawWord(Graphics graphics, ImageCreatingOptions options, string word, Rectangle rectangle)
        {
            graphics.DrawString(word, options.Font, options.Brush, rectangle);
        }

        public bool Check(string word) => true;
    }
}