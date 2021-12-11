#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Interfaces;

#endregion

namespace TagsCloudVisualization
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> GetWordsFromFile(string path, char[] separators)
        {
            if (!File.Exists(path)) throw new ArgumentException($"File {path} does not exist");

            var reader = new StreamReader(path);
            var words = new List<string>();
            var line = reader.ReadLine();

            while (!string.IsNullOrEmpty(line))
            {
                words.AddRange(line.Split(separators).Where(word => word.Length > 0));
                line = reader.ReadLine();
            }

            reader.Close();

            return words;
        }
    }
}