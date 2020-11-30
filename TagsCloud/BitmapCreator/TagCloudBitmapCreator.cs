using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TagsCloud.ImageConfig;
using TagsCloud.LayoutAlgorithms;
using TagsCloud.WordFilters;

namespace TagsCloud.BitmapCreator
{
    class TagCloudBitmapCreator : IBitmapCreator
    {
        private ILayoutAlgorithm _layoutAlgorithm;

        public TagCloudBitmapCreator(ILayoutAlgorithm layoutAlgorithm)
        {
            _layoutAlgorithm = layoutAlgorithm;
        }

        public Bitmap Create(IEnumerable<string> words)
        {
            throw new NotImplementedException();
        }

        // TODO вынести GetWordsFrequency в экстеншн и переназвать в GetFrequency.
        private IReadOnlyDictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            var result = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (result.ContainsKey(word))
                    result[word] += 1;
                else
                    result[word] = 1;
            }

            return result;
        }
    }
}
