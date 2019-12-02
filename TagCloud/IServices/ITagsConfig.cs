using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Models;

namespace TagCloud.IServices
{
    public interface ITagsConfig
    {
        Dictionary<string,int> PrimaryWordsCollection { get; }
        Dictionary<string,int> WordsCollectionAfterConvertion { get; }
        List<Tag> TagCollection { get; }
    }
}
