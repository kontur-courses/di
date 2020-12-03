using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.TextProcessing.TextReaders
{
    public class ReadersFactory : IReadersFactory
    {
        private readonly IWordsReader[] textReaders;
        private readonly WordConfig wordsConfig;

        public ReadersFactory(IEnumerable<IWordsReader> textReaders, WordConfig wordsConfig)
        {
            this.textReaders = textReaders.ToArray();
            this.wordsConfig = wordsConfig;
        }

        public IWordsReader CreateReader()
        {
            var reader = textReaders.FirstOrDefault(r => r.CanRead(wordsConfig.Path));
            if (reader == null)
                throw new InvalidOperationException("This file type is not supported");
            return reader;
        }
    }
}
