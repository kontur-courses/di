using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudLibrary.Readers
{
    public class TxtReader : IReader
    {
        public string Name { get; } = "txt";

        public Stream Read(Stream stream)
        {
            return stream;
        }
    }
}
