using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.App.Layouter
{
    public interface ICircularCloudLayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
        public void Clear();
    }
}
