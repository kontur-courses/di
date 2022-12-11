namespace TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;

public class NewLineTextSplitter : ITextSplitter
{
    public List<string> GetSplitWords(string text)
    {
        return text.Split(Environment.NewLine).ToList();
    }
}