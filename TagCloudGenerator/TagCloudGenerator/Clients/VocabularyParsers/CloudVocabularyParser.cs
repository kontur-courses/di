using System;
using System.Collections.Generic;
using System.IO;

namespace TagCloudGenerator.Clients.VocabularyParsers
{
    public abstract class CloudVocabularyParser
    {
        private readonly CloudVocabularyParser nextParser;

        protected CloudVocabularyParser(CloudVocabularyParser nextParser) => this.nextParser = nextParser;

        public IEnumerable<string> GetCloudVocabulary(string cloudVocabularyFilename)
        {
            if (!File.Exists(cloudVocabularyFilename))
                throw new FileNotFoundException("Specified file path doesn't exist", cloudVocabularyFilename);

            if (!VerifyFilename(cloudVocabularyFilename))
            {
                if (nextParser is null)
                    throw new NotSupportedException("Invalid vocabulary filename format");

                nextParser.GetCloudVocabulary(cloudVocabularyFilename);
            }

            var vocabularyFile = File.OpenText(cloudVocabularyFilename);

            return ParseCloudVocabulary(vocabularyFile);
        }

        protected abstract bool VerifyFilename(string filePath);
        protected abstract IEnumerable<string> ParseCloudVocabulary(StreamReader vocabularyFileStream);
    }
}