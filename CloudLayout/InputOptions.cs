using System.Drawing;

namespace CloudLayout
{
    public class InputOptions : IInputOptions
    {
        public Point CenterPoint { get; set; }
        public int Size { get; set; }
        public InputOptions(int size)
        {
            Size = size;
            CenterPoint = new Point(size / 2, size/ 2);
        }
    }

    public interface IInputOptions
    {
        Point CenterPoint { get; set; }

        int Size { get; set; }
    }
}