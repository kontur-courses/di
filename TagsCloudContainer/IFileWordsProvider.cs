using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public interface IFileWordsProvider
    {
        string[] GetWords(Stream stream);

        string[] AcceptedExtensions { get; }
    }
}
