using System.Collections.Generic;
using System.Linq;
using TagCloud.IServices;
using TagCloud.Models;

namespace TagCloud
{
    public class TagCollectionFactory : ITagCollectionFactory
    {
        private readonly IWordsToTagsParser parser;
        private readonly IWordsHandler wordsHandler;

        public TagCollectionFactory(IWordsHandler wordsHandler, IWordsToTagsParser parser)
        {
            this.wordsHandler = wordsHandler;
            this.parser = parser;
        }

        public List<Tag> Create(ImageSettings imageSettings, string path)
        {
            var primaryCollection = wordsHandler.GetWordsAndCount(path);
            var collectionAfterConversion = wordsHandler.Conversion(primaryCollection);
            return parser.GetTagsRectangles(collectionAfterConversion, imageSettings)
                .OrderByDescending(t => t.Count)
                .ToList();
        }
    }
}