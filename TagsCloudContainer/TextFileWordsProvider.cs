using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class TextFileWordsProvider : IFileWordsProvider
    {
        public string[] GetWords(Stream stream)
        {
            var words = new List<string>();
            using (var streamReader = new StreamReader(stream, true))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                    words.Add(line);
            }

            return words.ToArray();
        }

        public string[] AcceptedExtensions => new[] {"txt"};
    }
}
