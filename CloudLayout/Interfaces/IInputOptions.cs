using System.Drawing;

namespace CloudLayout.Interfaces
{
    public interface IInputOptions
    {
        Point CenterPoint { get; set; }

        int Size { get; set; }
    }
}