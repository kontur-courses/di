using System.Collections.Generic;
using System.Drawing;
using TagsCloudTextProcessing;
using TagsCloudVisualization.Styling;

namespace TagsCloudVisualization.Layouters
{
    public interface ICloudLayouter
    {
        RectangleF PutNextRectangle(SizeF rectangleSize);

        Tag PutNextTag(Token token, SizeF tokenSize);

        IEnumerable<Tag> GenerateTagsSequence(Style style, IEnumerable<Token> tokens);
    }
}