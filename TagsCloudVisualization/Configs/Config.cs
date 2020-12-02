using System.Drawing;

namespace TagsCloudVisualization
{
    public class Config : IConfig //TODO rename
    {
        public Font Font { get; }//toDo rename
        public Point Center { get; }
        
        public Color TextColor { get; }

        public Config(Font font, Point center, Color textColor)
        {
            Font = font;
            Center = center;
            TextColor = textColor;
        }
    }
}