using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public interface IVisualizer
    {
        Bitmap Save();
        void Clear();
        void DrawTag(TagRectangle tagRectangle, Font font);
    }
}