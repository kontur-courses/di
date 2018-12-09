using System.Drawing;

namespace TagCloudVisualization
{
    public interface IWordDrawer
    {
        void DrawWord(Graphics graphics, ImageCreatingOptions options, string word, Rectangle rectangle);
        bool Check(string word);

    }
}