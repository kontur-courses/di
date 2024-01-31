namespace TagsCloudContainer;

public class WordDetails
{
    public string Word { get; }
    
    public int Frequency { get; set; }
    
    public string? SpeechPart { get; }

    public WordDetails(string word, int frequency = 1, string? speechPart = null)
    {
        Word = word;
        Frequency = frequency;
        SpeechPart = speechPart;
    }
    
    public static bool CanMap(string wordInfo)
    {
        var data = wordInfo.Split('=');
        if (data.Length < 2)
            return false;
        
        var word = data[0];
        
        return !word.Contains("??");
    }
}