using System.Linq;

namespace TagsCloudVisualization
{
    public static class StringExtension
    {
        public static string ExtractFileExtension(this string fileName)
        {
            if (!fileName.Contains('.'))
                return null;
            return fileName.Split('.').LastOrDefault();
        }
    }
}
