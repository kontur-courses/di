namespace TagsCloudContainer.TextAnalysers;

public class MyStemParser: IMyStemParser
{
    public bool CanParse(string wordInfo)
    {
        var wordInfoSegments = ParseWordInfo(wordInfo);
        if (wordInfoSegments is null) 
            return false;

        var word = wordInfoSegments[0];
        return !word.Contains("??");
    }

    public WordDetails Parse(string wordInfo)
    {
        var wordInfoSegments = ParseWordInfo(wordInfo);
        if (wordInfoSegments is null) 
            throw new ArgumentException(nameof(wordInfo));

        var word = wordInfoSegments[0];
        if (word.Contains("??"))
            throw new ArgumentException(nameof(wordInfo));

        var speechPart = wordInfoSegments[1].Split(',').First();
        return new WordDetails(word, speechPart: speechPart);
    }

    private string[]? ParseWordInfo(string wordInfo)
    {
        var wordInfoSegments = wordInfo.Split('=');
        return wordInfoSegments.Length >= 2 ? wordInfoSegments : null;
    }
}