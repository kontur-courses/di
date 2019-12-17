using System.Drawing;

namespace CloudDrawing
{
    public interface ICircularCloudDrawing
    {
        void DrawString(string str, Font font, Brush brush, StringFormat stringFormat);

        void DrawRectangle(Rectangle rectangle, Pen pen);

        void SaveImage(string filename);
        void SetOptions(Color background, Size imageSize);
    }
}