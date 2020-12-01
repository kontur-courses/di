using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IWordConfig //TODo переместить в папку
    {
        Font Font { get; }
        Point Center { get; }
        
        Color TextColor { get; }
    }
}