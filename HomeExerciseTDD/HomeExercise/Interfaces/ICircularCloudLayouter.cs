using System.Drawing;

namespace HomeExerciseTDD
{
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}