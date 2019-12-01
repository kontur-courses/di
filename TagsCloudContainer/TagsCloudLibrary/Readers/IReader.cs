using System.Collections.Generic;
using System.IO;

namespace TagsCloudLibrary.Readers
{
    public interface IReader
    {
        string Name { get; }
        Stream Read(Stream stream);
    }
}
