using System.Drawing;

namespace HomeExerciseTDD
{
    public interface ISpiral
    {
        public Point Center { get; }
        Point GetNextPoint();
    }
}