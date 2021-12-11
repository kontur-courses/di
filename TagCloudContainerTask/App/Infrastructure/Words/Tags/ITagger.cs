using System.Collections.Generic;
using App.Implementation.Words.Tags;

namespace App.Infrastructure.Words.Tags
{
    public interface ITagger
    {
        IEnumerable<Tag> CreateRawTags(Dictionary<string, double> wordsFrequencies);
    }
}