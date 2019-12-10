using System.Drawing;
using TagsCloudTextProcessing;

namespace TagsCloudVisualization.Layouters
{
    public interface ICloudLayouter
    { 
        RectangleF PutNextRectangle(SizeF rectangleSize);
        
        Tag PutNextTag(Token token, SizeF tokenSize);
    }
}