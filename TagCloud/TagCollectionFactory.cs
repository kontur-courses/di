using System.Linq;
using TagCloud.IServices;

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

        public TagCollection Create(string path)
        {
            var primaryCollection = wordsHandler.GetWordsAndCount(path);
            var collectionAfterConvertion = wordsHandler.Conversion(primaryCollection);
            return new TagCollection(parser.GetTagsRectangles(collectionAfterConvertion)
                .OrderByDescending(t => t.Count)
                .ToList());
        }
    }
}