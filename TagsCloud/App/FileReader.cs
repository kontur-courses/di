using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloud.App
{
    public abstract class FileReader
    {
        public static bool SplitLines;
        public virtual HashSet<string> AvailableFileTypes { get; }

        protected void CheckForExceptions(string fileName)
        {
            var fileType = fileName.Split('.')[^1];
            if (!AvailableFileTypes.Contains(fileType))
                throw new ArgumentException($"Incorrect type {fileType}");
        }

        protected string[] SplitLine(string line)
        {
            return new Regex("\\W+").Split(line);
        }


        protected IEnumerable<string> GetWords(string line)
        {
            if (SplitLines)
                foreach (var word in SplitLine(line))
                    yield return word;
            else yield return line;
        }

        public abstract IEnumerable<string> ReadLines(string fileName);
    }
}