﻿using System.Collections.Generic;
using System.Linq;
using TagCloudContainerTests;

namespace TagsCloudContainer
{
    public class Normalizator : IPreprocessor
    {
        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
        {
            return tags.Select(t => Normalize(t));
        }

        private SimpleTag Normalize(SimpleTag tag)
        {
            var processedWord = tag.Word.ToLower();
            return new SimpleTag(processedWord, tag.Count);
        }
    }
}