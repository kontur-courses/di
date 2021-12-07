using System.Drawing;

namespace TagsCloudVisualizationTests.Interfaces
{
    public interface IRectangleStyle
    {
        public void Draw(Graphics graphics, Rectangle rectangle);
    }
}