using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.WordsFilter.BoringWords
{
    public class BoringWords : IBoringWords
    {
        private readonly string fileName;

        public BoringWords(string fileName)
        {
            this.fileName = fileName;
        }

        public HashSet<string> GetBoringWords
        {
            get
            {
                if (!File.Exists(fileName))
                    return new HashSet<string>();

                var words = File.ReadAllLines(fileName);

                return new HashSet<string>(words.Select(word => word.ToLower()));
            }
        }
    }
}
