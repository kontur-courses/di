using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class TagsPreprocessor
    {
        private readonly IPreprocessor[] preprocessors;

        public TagsPreprocessor(IPreprocessor[] preprocessors)
        {
            this.preprocessors = (from preprocessor in preprocessors
                    let prop = preprocessor.GetType()
                        .GetProperty(nameof(State))
                    where (State) prop.GetValue(null) == State.Active
                    select preprocessor)
                .ToArray();
        }

        public List<SimpleTag> Process(IEnumerable<SimpleTag> tags) 
            => preprocessors.Aggregate
                (tags, (current, preprocessor) => preprocessor.Process(current))
                .ToList();
    }
}