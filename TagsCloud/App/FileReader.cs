using System;
using System.Collections.Generic;

namespace TagsCloud.App
{
    public abstract class FileReader
    {
        public virtual HashSet<string> AvailableFileTypes { get; }

        protected void CheckForExceptions(string fileName)
        {
            var fileType = fileName.Split('.')[^1];
            if (!AvailableFileTypes.Contains(fileType))
                throw new ArgumentException($"Incorrect type {fileType}");
        }

        public abstract string[] ReadLines(string fileName);
    }
}