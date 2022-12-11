using TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;

namespace TagCloud.App.WordPreprocessorDriver.InputStream;

public interface IInputWordsStream
{
    List<string> GetAllWordsFromStream(IStreamContext streamContext, ITextSplitter textSplitter);
}