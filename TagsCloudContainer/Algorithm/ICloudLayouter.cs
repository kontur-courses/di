using TagsCloudContainer.Infrastucture;

namespace TagsCloudContainer.Algorithm
{
    public interface ICloudLayouter
    {
        List<TextRectangle> GetRectangles(Dictionary<string, int> wordFrequencies);
    }
}