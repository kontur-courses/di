using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface ICircularCloudLayouter
    {
        public Point CloudCenter { get; init; }
        public IList<Rectangle> Rectangles { get; init; }
        Rectangle PutNextRectangle(string word, Font font);
    }
}
