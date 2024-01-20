using System.Drawing;
using TagsCloudContainer.Image;

namespace TagsCloudContainer.TagCloud;

public class TagCloudVisualizer(ICircularCloudLayouter circularCloudLayouter, ImageGenerator imageGenerator)
{
    public void GenerateTagCloud(IEnumerable<(string word, int count)> frequencyDict)
    {
        var wordsFrequenciesOutline = new List<(string word, int frequency, Rectangle outline)>();
        foreach (var kvp in frequencyDict)
        {
            var rectangle =
                circularCloudLayouter.PutNextRectangle(imageGenerator.GetOuterRectangle(kvp.word, kvp.count));
            wordsFrequenciesOutline.Add((kvp.word, kvp.count, rectangle));
        }

        imageGenerator.DrawTagCloud(wordsFrequenciesOutline);
    }
}