using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagsCloudLayouter
    {
        ITagsCloud TagsCloud { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
        TagsCloudWord PutNextWord(string word, Size WordSize);
    }
}