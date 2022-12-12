using System;
using System.IO;

namespace TagCloud.Files;

public interface IFileReader
{
    public string Extension { get; }
    public string ReadAll(string filename);
}