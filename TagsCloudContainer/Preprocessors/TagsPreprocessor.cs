using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class TagsPreprocessor
    {
        private readonly IPreprocessor[] preprocessors;

        public TagsPreprocessor(IPreprocessor[] preprocessors)
        {
            this.preprocessors = preprocessors;
        }

        public List<SimpleTag> Process(IEnumerable<SimpleTag> tags) 
            => preprocessors.Aggregate
                (tags, (current, preprocessor) => preprocessor.Process(current))
                .ToList();
    }
}