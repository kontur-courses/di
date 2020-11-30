using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.TextProcessing.TextReaders
{
    public class ReadersFactory : IReadersFactory
    {
        private readonly IWordsReader[] textReaders;

        public ReadersFactory(IEnumerable<IWordsReader> textReaders)
        {
            this.textReaders = textReaders.ToArray();
        }

        public IEnumerable<string> ReadText(string path)
        {
            var reader = textReaders.FirstOrDefault(r => r.CanRead(path));
            if (reader == null)
                throw new InvalidOperationException("This file type is not supported");
            return reader.ReadWords(path);
        }
    }
}
