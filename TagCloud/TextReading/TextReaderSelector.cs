using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagCloud.TextReading
{
    public class TextReaderSelector : ITextReaderSelector
    {
        private readonly IEnumerable<ITextReader> textReaders;

        public TextReaderSelector(IEnumerable<ITextReader> textReaders)
        {
            this.textReaders = textReaders;
        }

        public ITextReader GetTextReader(FileInfo file)
        {
            var reader = textReaders.FirstOrDefault(r => r.Extension == file.Extension);
            if (reader == null)
                throw new NotSupportedException("Files of this type are not supported");
            return reader;
        }
    }
}