using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class WordPreparer : IWordPreparer
    {
        public string[] Prepare(IEnumerable<string> words)
        {
            ArgumentNullException.ThrowIfNull(words, nameof(words));

            return words.Select(word => word.ToLower()).ToArray();       
        }
    }
}