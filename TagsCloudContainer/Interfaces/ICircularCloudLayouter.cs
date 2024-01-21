using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface ICircularCloudLayouter
    {
        public Point CloudCenter { get; }
        public IList<Rectangle> Rectangles { get; }
        Rectangle PutNextRectangle(string word, Font font);
    }
}
