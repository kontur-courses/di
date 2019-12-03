using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.IServices;

namespace TagCloud
{
    public class TagCollectionFactory : ITagCollectionFactory
    {
        private readonly IWordsHandler wordsHandler;
        private readonly IWordsToTagsParser parser;

        public TagCollectionFactory(IWordsHandler wordsHandler, IWordsToTagsParser parser)
        {
            this.wordsHandler = wordsHandler;
            this.parser = parser;
        }
        public TagCollection Create(string path)
        {
            var primaryCollection = wordsHandler.GetWordsAndCount(path);
            var collectionAfterConvertion = wordsHandler.Conversion(primaryCollection);
            return  new TagCollection(parser.GetTagsRectangles(collectionAfterConvertion));
        }
    }
}
