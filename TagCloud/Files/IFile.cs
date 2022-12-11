using System;
using System.IO;

namespace TagCloud.Files;

public interface IFile
{
    public string Path { get; }
    public string ReadAll();

    public static IFile GetByFilename(string filename)
    {
        var extension = System.IO.Path.GetExtension(filename);
        return extension.ToLower() switch
        {
            TxtFile.Extension => new TxtFile(filename),
            DocFile.Extension => new DocFile(filename),
            _ => throw new ArgumentException($"This extension {extension} are not supported!")
        };
    }
}