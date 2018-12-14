using System.Drawing;
using System.Windows.Forms;

namespace TagCloud.ColorPicker
{
    public class BrightnessColorPicker : IColorPicker
    {
        public Color AdjustColor(Color baseColor, float usingFrequency)
        {
            return ControlPaint.Light(baseColor, (usingFrequency - 0.5f) / 2);
        }
    }
}