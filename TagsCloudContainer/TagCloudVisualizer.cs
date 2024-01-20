using System.Drawing;

namespace TagsCloudContainer;

public class TagCloudVisualizer(CircularCloudLayouter circularCloudLayouter,
    ImageGenerator imageGenerator)
{
    public void GenerateTagCloud(WordsDataSet wordsDataSet)
    {
        var wordsFrequenciesOutline = new List<(string word, int frequency, Rectangle outline)>();
        foreach (var kvp in wordsDataSet.CreateFrequencyDict())
        {
            var rectangle =
                circularCloudLayouter.PutNextRectangle(imageGenerator.GetOuterRectangle(kvp.word, kvp.count));
            wordsFrequenciesOutline.Add((kvp.word, kvp.count, rectangle));
        }

        imageGenerator.DrawTagCloud(wordsFrequenciesOutline);
    }
}