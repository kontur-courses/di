using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TagsCloud_Test")]

namespace TagCloud.Layout
{
    internal interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        void Reset();
    }
}