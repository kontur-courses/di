using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;

namespace TagCloud.App.WordPreprocessorDriver.InputStream;

public class FromFileInputWordsStream
{
    private readonly ITextSplitter textSplitter;

    public FromFileInputWordsStream(ITextSplitter textSplitter)
    {
        this.textSplitter = textSplitter;
    }

    public List<string> GetAllWordsFromStream(
        FromFileStreamContext streamContext)
    {
        CheckFile(streamContext.Filename, streamContext.FileEncoder);
        return FillWordsFromFile(streamContext.Filename, streamContext.FileEncoder, textSplitter);
    }

    private static void CheckFile(string filename, IFileEncoder selectedFileEncoder)
    {
        if (!File.Exists(filename))
            throw new FileNotFoundException($"File was not found at {filename}");
        if (!selectedFileEncoder.IsCompatibleFileType(filename))
            throw new Exception($"Expected {selectedFileEncoder.GetExpectedFileType()} filetype, " +
                                $"but was found {filename.Split('.').LastOrDefault() ?? string.Empty}");
    }

    private static List<string> FillWordsFromFile(string filename, IFileEncoder fileEncoder,
        ITextSplitter splitter)
    {
        string text;
        try
        {
            text = fileEncoder.GetText(filename);
        }
        catch (ArgumentException e)
        {
            throw new Exception("Can not get data from file", e);
        }

        return splitter.GetSplitWords(text).Where(s => s.Length > 0).ToList();
    }
}