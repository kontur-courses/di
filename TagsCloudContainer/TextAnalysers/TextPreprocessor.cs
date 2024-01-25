using MyStemWrapper;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.TextAnalysers;

public class TextPreprocessor: ITextPreprocessor
{
    private readonly CloudData cloudData;
    private readonly MyStem myStem;
    private readonly AnalyseSettings settings;

    public TextPreprocessor(CloudData cloudData, MyStem myStem, AnalyseSettings settings)
    {
        this.cloudData = cloudData;
        this.myStem = myStem;
        this.settings = settings;
    }

    public CloudData Preprocess(string text)
    {
        var s = myStem.Analysis(text);
        var wordInfos = s.Split('\n');
        var words = wordInfos
            .Where(inf => !CheckWordIsBoring(inf))
            .Select(inf => inf.Split('=').First().ToLower());
        UpdateCloud(words);
        
        return cloudData;
    }
    
    private void UpdateCloud(IEnumerable<string> words)
    {
        var frequency = new Dictionary<string, int>();
        foreach (var word in words)
        {
            frequency.TryAdd(word, 0);
            frequency[word] += 1;
        }

        cloudData.WordsFrequency = frequency;
    }

    private bool CheckWordIsBoring(string wordInfo)
    {
        if (string.IsNullOrWhiteSpace(wordInfo))
            return true;
        
        var data = wordInfo.Split('=');
        if (data.Length < 2)
            return true;
        
        var word = data[0];
        if (word.Contains("??"))
            return true;

        var speechType = data[1];

        return !settings.ValidParts.Contains(speechType);
    }
}