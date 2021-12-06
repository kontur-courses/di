using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace TagCloud.file_readers
{
    public class TxtReader : IFileReader
    {
        private readonly string filename;
        private readonly List<string> words;

        public TxtReader(string filename)
        {
            this.filename = filename;
            words = new List<string>();
            InitWords();
        }

        public IEnumerable<string> GetWords() => words;
        
        private void InitWords()
        {
            try
            {
                using var sr = new StreamReader(filename);
                var line = sr.ReadLine();
                while (line is not null)
                {
                    words.Add(line);
                    line = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Error($"The file could not be read\n{e.Message}");
                throw;
            }
        }
    }
}