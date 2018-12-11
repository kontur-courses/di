using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Painter
{
    public class PainterManager
    {
        private readonly ICloudColorPainter[] painters;

        public PainterManager(ICloudColorPainter[] painters)
        {
            this.painters = painters;
        }

        //public ICloudColorPainter GetInstance()
    }
}