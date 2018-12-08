using System;
using System.IO;

namespace TagsCloudPreprocessor
{
    public class TxtReader:IReader
    {
        public string GetTextFromRawFormat(string rawText)
        {
            return rawText;
        }
    }
}
