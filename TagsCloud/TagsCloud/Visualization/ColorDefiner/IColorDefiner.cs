using System.Drawing;

namespace TagsCloud.Visualization.ColorDefiner
{
    public interface IColorDefiner
    {
        Color DefineColor(int frequency);
    }
}