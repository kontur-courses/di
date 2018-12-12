using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ISizeDefiner
    {
        Size GetSize(GraphicWord graphicWord);
    }
}
