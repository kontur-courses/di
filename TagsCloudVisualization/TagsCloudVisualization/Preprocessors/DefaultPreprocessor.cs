namespace TagsCloudVisualization;

public class DefaultPreprocessor : IPreprocessor
{
    public Dictionary<string, int> Preprocessing(string text)
    {
        Dictionary<string, int> result = new();
        text = text.ToLower();
        var words = text.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var word in words)
        {
            if (result.ContainsKey(word))
                result[word]++;
            else
                result.Add(word, 1);
        }

        return result;
    }
}