using System.Collections.Generic;
using TagCloud.IServices;
using TagCloud.Models;

namespace TagCloud
{
    public class TagsConfig : ITagsConfig
    {
        
        public TagsConfig(IWordsHandler wordsHandler,IWordsToTagsParser wordsToTagsParser)
        {
            PrimaryWordsCollection = wordsHandler.GetWordsAndCount(Settings.PathToFileWithWords);
            WordsCollectionAfterConvertion = wordsHandler.Conversion(PrimaryWordsCollection);
            TagCollection = wordsToTagsParser.GetTagsRectangles(WordsCollectionAfterConvertion);
        }

        public Dictionary<string, int> PrimaryWordsCollection { get; }

        public Dictionary<string, int> WordsCollectionAfterConvertion { get; }

        public List<Tag> TagCollection { get; }
    }
}
