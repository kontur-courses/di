using System.Drawing;

namespace WinUI.InputModels
{
    public class UserInputSizeField
    {
        public UserInputSizeField(string description)
        {
            Description = description;
        }

        public string Description { get; }
        public int Width { get; set; }
        public int Height { get; set; }
        
        public Point PointFromCurrent() => new Point(Width, Height);
        public Size SizeFromCurrent() => new Size(Width, Height);
    }
}