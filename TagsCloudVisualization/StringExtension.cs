using System;
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

        public static int SkipUntil(this string text, int startPos, Func<char, bool> predicate)
        {
            var currentPos = startPos;
            while (currentPos < text.Length)
            {
                if (predicate(text[currentPos]))
                    return currentPos;
                currentPos++;
            }

            return currentPos;
        }
    }
}
