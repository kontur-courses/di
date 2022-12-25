using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.WordsParser.FileReaders
{
    public class TxtReader : IFileReader
    {
        private readonly StreamReader _streamReader;

        public TxtReader(string filePath)
        {
            _streamReader = new StreamReader(filePath);
        }

        public string? ReadWord()
        {
            while (_streamReader.Peek() >= 0)
            {
                var word = _streamReader.ReadLine()?.Trim();

                if (word is null)
                    return null;

                if (word.Length != 0)
                    return word;
            }

            return null;
        }
    }
}