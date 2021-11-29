using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.WordLoading
{
    public class NewLineWordLoader
    {
        public IEnumerable<string> Load(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new ApplicationException($"File not exist: {filePath}");

            return File.ReadAllLines(filePath);
        }
    }
}