using System.Drawing;
using CloudLayout.Interfaces;

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
}