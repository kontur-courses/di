using System.Drawing;
using TagsCloudTextPreparation;

namespace TagsCloudVisualization.Layouters
{
    public interface ICloudLayouter
    { 
        RectangleF PutNextRectangle(SizeF rectangleSize);
        
        Tag PutNextTag(FrequencyWord word, SizeF wordSize);
    }
}