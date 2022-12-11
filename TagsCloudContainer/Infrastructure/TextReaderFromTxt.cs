using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudContainer.Infrastructure
{
    public class TextReaderFromTxt : ITextReader
    {
        public string Filter => "Изображения (*.txt)|*.txt";

        public string ReadText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
