using MyStemWrapper;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.TextAnalysers;

public class TextPreprocessor: ITextPreprocessor
{
    private readonly MyStem myStem;
    private readonly IAnalyseSettings settings;

    public TextPreprocessor(MyStem myStem, IAnalyseSettings settings)
    {
        this.myStem = myStem;
        this.settings = settings;
    }

    public AnalyzeData Preprocess(string text)
    {
        var analyzed = myStem.Analysis(text);
        var wordInfos = analyzed.Split('\n');
        var words = wordInfos
            .Where(info => !CheckWordIsBoring(info) && WordData.CanMap(info))
            .Select(info => info.Split('=').First().ToLower());
        
        var wordsFrequency = CalculateFrequency(words);
        var wordData = wordsFrequency
            .Select(pair => new WordData(pair.Key, pair.Value));
        
        return new AnalyzeData
        {
            WordData = wordData.ToArray(),
        };
    }
    
    private Dictionary<string,int> CalculateFrequency(IEnumerable<string> words)
    {
        var frequency = new Dictionary<string, int>();
        foreach (var word in words)
        {
            frequency.TryAdd(word, 0);
            frequency[word] += 1;
        }

        return frequency;
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

        var speechType = data[1].Split(',').First();

        return !settings.ValidSpeechParts.Contains(speechType);
    }
}