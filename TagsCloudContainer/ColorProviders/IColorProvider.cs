using System.Drawing;

namespace TagsCloudContainer
{
    public interface IColorProvider
    {
        public Color GetNextColor();
    }
}