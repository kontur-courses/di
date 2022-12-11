using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;

namespace TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;

public class FromFileInputWordsStream : IInputWordsStream
{
    private string? fileName;
    private IFileEncoder? fileEncoder;
    private ITextSplitter? splitter;
        
    private List<string>? words;
    private int currentItem = -1;

    public IInputWordsStream OpenFile(string filename, IFileEncoder selectedFileEncoder)
    {
        fileName = filename;
        fileEncoder = selectedFileEncoder;
        SelectFile(filename, fileEncoder);
        return this;
    }

    public IInputWordsStream UseSplitter(ITextSplitter textSplitter)
    {
        splitter = textSplitter;
        return this;
    }
        
    private void SelectFile(string filename, IFileEncoder selectedFileEncoder)
    {
        fileName = filename;
        if (!File.Exists(fileName))
            throw new FileNotFoundException($"File was not found at {fileName}");
        if (!selectedFileEncoder.IsCompatibleFileType(fileName))
            throw new Exception($"Expected {selectedFileEncoder.GetExpectedFileType()} filetype, " +
                                $"but was found {fileName.Split('.').LastOrDefault() ?? string.Empty}");
    }

    private bool IsSomeFieldsNotInitialised()
    {
        return splitter == null
               || fileEncoder == null
               || fileName == null;
    }

    public bool MoveNext()
    {
        if (IsSomeFieldsNotInitialised())
            return false;
        words ??= FillWordsFromFile(fileName!, fileEncoder!, splitter!);
        return ++currentItem < words.Count;
    }
        
    public string GetWord()
    {
        if (IsSomeFieldsNotInitialised())
            throw new Exception("At first you should OpenFile() and UseSplitter()");
        if (currentItem < 0)
            throw new Exception("At first you should call Next()");
        if (currentItem >= words!.Count)
            throw new EndOfStreamException("File has not more words");
        return words[currentItem];
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
            throw new ArgumentException("Can not get data from file", e);
        }
        return splitter.GetSplitWords(text).Where(s => s.Length > 0).ToList();
    }
}