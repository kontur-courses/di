using System.IO;

namespace TagCloud.TextReading
{
    public interface ITextReaderSelector
    {
        ITextReader GetTextReader(FileInfo file);
    }
}
