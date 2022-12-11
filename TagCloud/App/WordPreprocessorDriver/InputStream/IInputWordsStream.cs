using TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;

namespace TagCloud.App.WordPreprocessorDriver.InputStream;

public interface IInputWordsStream
{
    IInputWordsStream OpenFile(string filename, IFileEncoder selectedFileEncoder);
        
    IInputWordsStream UseSplitter(ITextSplitter textSplitter);
        
    bool MoveNext();
        
    string GetWord();
}