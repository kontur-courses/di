﻿namespace TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;

public class FromFileStreamContext
{
    public readonly string Filename;
    public readonly IFileEncoder FileEncoder;
    
    public FromFileStreamContext(string filename, IFileEncoder fileEncoder)
    {
        Filename = filename;
        FileEncoder = fileEncoder;
    }
}