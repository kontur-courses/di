using System.Drawing;

namespace TagsCloudContainer.Layouter
{
    public abstract class MustInitialize<T>
    {
        public MustInitialize(T parameters)
        {

        }
    }
    
    public interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}