using System.Collections.Generic;
using System.Linq;
using TagCloudContainerTests;

namespace TagsCloudContainer
{
    public class TagsFilter : IPreprocessor
    {
        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
        {
            // Не уверен как это реализовать, в лоб не хочется, так что пока по минимуму
            return tags.Where(t => IsRelevantTag(t));
        }

        private bool IsRelevantTag(SimpleTag tag)
            => tag.Word.Length > 2;
    }
}