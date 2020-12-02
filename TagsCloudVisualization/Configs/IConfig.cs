using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IConfig //TODo переместить в папку
    {
        Font Font { get; }
        Point Center { get; }
        
        Color TextColor { get; }

        public void SetValues(Font font, Point center, Color textColor);
    }
}