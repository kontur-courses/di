using System.Collections.Generic;
using System.IO;

namespace TagCloudGenerator.UserInterfaces.VocabularyParsers
{
    public class TxtVocabularyParser : CloudVocabularyParser
    {
        private const string FileExtension = ".txt";

        public TxtVocabularyParser(CloudVocabularyParser nextParser) : base(nextParser) { }

        protected override bool VerifyFilePath(string filePath) => Path.GetExtension(filePath) == FileExtension;

        protected override IEnumerable<string> ParseCloudVocabulary(StreamReader vocabularyFileStream)
        {
            while (!vocabularyFileStream.EndOfStream)
                yield return vocabularyFileStream.ReadLine();
        }
    }
}