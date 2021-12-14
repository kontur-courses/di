using System;
using System.IO;

namespace Visualization
{
    public class WordsParser : IWordsParser
    {
        public string[] Read(string fullString)
        {
            return fullString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}