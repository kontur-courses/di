using System;
using System.IO;
using System.Linq.Expressions;

namespace TextPreprocessor.TextRiders
{
    public class TextRiderConfig
    {
        public string FilePath;
        public Func<string, string> WordFormat;
        public Func<string, bool> WordFilter;

        public static TextRiderConfig Default()
        {
            return new TextRiderConfig()
            {
                FilePath = @"D:\Git-Repos\test.txt",
                WordFormat = word => word.ToLower(),
                WordFilter = word => true,
            };
        }
    }
}