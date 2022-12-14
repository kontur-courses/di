using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure.WordPreparers
{
    public class WordPreparer : IWordPreparer
    {
        private readonly WordType[] excludedTypes;

        public WordPreparer(WordType[] excludedTypes)
        {
            this.excludedTypes = excludedTypes;
        }

        public string[] Prepare(IEnumerable<string> words)
        {
            ArgumentNullException.ThrowIfNull(words, nameof(words));

            return words.Select(word => word.ToLower())
                        .Where(word => !excludedTypes.Contains(OpenNLPPOSFacade.GetWordType(word)))
                        .ToArray();       
        }
    }
}