using System;
using System.IO;

namespace TagsCloudVisualization
{
    internal static class TextReader
    {
        public static string[] Read(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllLines(path);
            }

            throw new System.IO.FileNotFoundException("File at the specified path does not exist");
        }
    }
}
