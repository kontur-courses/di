using System.Drawing;
using TagsCloudTextPreparation;

namespace TagsCloudVisualization.Layouters
{
    public interface ICloudLayouter
    { 
        RectangleF PutNextRectangle(SizeF rectangleSize);
        
        Tag PutNextTag(Token word, SizeF wordSize);
    }
}