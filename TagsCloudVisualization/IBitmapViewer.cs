using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public interface IBitmapViewer
    {
        void View(Bitmap bitmap);
    }
}