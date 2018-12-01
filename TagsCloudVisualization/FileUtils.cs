using System;

namespace TagsCloudVisualization
{
    public static class FileUtils
    {
        public const string PngExtension = "png";

        public static string FindFreeFileName(string extension)
        {
            return $"{Guid.NewGuid().ToString()}.{extension}";
        }
    }
}
