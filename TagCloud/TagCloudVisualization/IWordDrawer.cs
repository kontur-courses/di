using System.Drawing;

namespace TagCloudVisualization
{
    public interface IWordDrawer
    {
        void DrawWord(Graphics graphics, ImageCreatingOptions options, WordInfo wordInfo, Font font);

        /// <summary>
        ///     Checks if this drawer is meant to draw this word
        /// </summary>
        bool Check(WordInfo wordInfo);
    }
}
