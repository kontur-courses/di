using System.Collections.Generic;

namespace TagsCloudContainer.Common.Contracts
{
    public interface IFileReader
    {
        string[] SupportedFormats { get; }

        string ReadFile(string path);

        IEnumerable<string> ReadLines(string path);
    }
}