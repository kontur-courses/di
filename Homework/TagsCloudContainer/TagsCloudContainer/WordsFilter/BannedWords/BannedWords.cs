using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.WordsFilter.BannedWords
{
    public class BannedWords : IBannedWords
    {
        private readonly string fileName;

        public BannedWords(string fileName)
        {
            this.fileName = fileName;
        }

        public HashSet<string> GetBannedWords
        {
            get
            {
                if (fileName == "")
                    return new HashSet<string>();

                var words = File.ReadAllLines(fileName);

                return new HashSet<string>(words.Select(word => word.ToLower()));
            }
        }
    }
}
