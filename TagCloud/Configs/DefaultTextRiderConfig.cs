using System;
using System.IO;

namespace TagCloud
{
    public class DefaultTextRiderConfig : ITextRiderConfig
    {
        public string FilePath => @"С:\TestTexts\test.txt";
        public Func<string, string> WordFormat => word => word.ToLower();
        public Func<string, bool> WordFilter => word => true;
    }
}