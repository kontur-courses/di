using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;

namespace TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;

public class FromFileInputWordsStream : IInputWordsStream
{
    public List<string> GetAllWordsFromStream(IStreamContext streamContext, ITextSplitter textSplitter)
    {
        if (textSplitter == null) throw new ArgumentNullException(nameof(textSplitter));
        if (streamContext is not FromFileStreamContext fileStreamContext)
            throw new Exception($"Incorrect type of settings. Expected {typeof(FromFileStreamContext)}, " +
                                $"but was found {streamContext.GetType()}");
        
        CheckFile(fileStreamContext.Filename, fileStreamContext.FileEncoder);
        return FillWordsFromFile(fileStreamContext.Filename, fileStreamContext.FileEncoder, textSplitter);
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