namespace TagCloud.Common.Options;

public class WordsOptions
{
    public WordsOptions(int boringWordsThreshold, int minFontSize, string pathToTextFile)
    {
        BoringWordsThreshold = boringWordsThreshold;
        MinFontSize = minFontSize;
        PathToTextFile = pathToTextFile;
    }

    public int BoringWordsThreshold { get; }
    public int MinFontSize { get; }
    public string PathToTextFile { get; }
}