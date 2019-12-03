using System.Drawing;
using TagsCloudTextPreparation;
using TagsCloudVisualization.Styling.WordSizeCalculators;

namespace TagsCloudVisualization
{
    public interface ICloudLayouter
    { 
        RectangleF PutNextRectangle(SizeF rectangleSize);
        
        Tag PutNextTag(FrequencyWord word, SizeF wordSize);
    }
}