using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm
{
    public interface IGraphicDrawer : IDisposable
    { 

        void DrawRectangle(Pen rectPen, Rectangle rectangle);

        void FillRectangle(Brush brush, Rectangle rectangle);

        void FillRectangle(Brush brush, int x, int y, int width, int height);

        void DrawString(string word, Font font, Brush textBrush, PointF point);
    }
}
