namespace TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;

public interface ITextSplitter
{
    List<string> GetSplitWords(string text);
}