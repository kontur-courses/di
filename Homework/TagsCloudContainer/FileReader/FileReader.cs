﻿using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.FileReader
{
    public class TxtFileReader : IFileReader
    {
        public TextFileFormat Format => TextFileFormat.Txt;

        public IEnumerable<string> ReadWords(string path)
        {
            if (!File.Exists(path))
                throw new Exception("File is not exist");
            using var reader = new StreamReader(path);
            while (reader.Peek() >= 0)
                yield return reader.ReadLine()!;
        }
    }
}