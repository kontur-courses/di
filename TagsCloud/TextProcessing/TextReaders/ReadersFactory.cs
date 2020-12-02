using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TextProcessing.TextReaders
{
    public class ReadersFactory : IReadersFactory
    {
        private readonly IWordsReader[] textReaders;
        private readonly IWordsConfig wordsConfig;

        public ReadersFactory(IEnumerable<IWordsReader> textReaders, IWordsConfig wordsConfig)
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
