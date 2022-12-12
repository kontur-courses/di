using System;
using System.IO;

namespace TagCloud.Files;

public interface IFile
{
    public string Path { get; set; }
    public string ReadAll();
}