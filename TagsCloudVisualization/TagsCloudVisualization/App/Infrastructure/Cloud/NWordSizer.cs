using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class NWordSizer : ISizeDefiner
    {
        public Size GetSize(GraphicWord graphicWord)
        {
            return TextRenderer.MeasureText(graphicWord.Value, graphicWord.Font);
        }
    }
}