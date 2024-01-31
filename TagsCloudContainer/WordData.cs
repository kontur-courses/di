namespace TagsCloudContainer;

public class WordData
{
    public string Word { get; }
    
    public int Frequency { get; }

    public WordData(string word, int frequency)
    {
        Word = word;
        Frequency = frequency;
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