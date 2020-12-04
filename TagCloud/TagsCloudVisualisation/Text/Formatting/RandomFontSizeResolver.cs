﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualisation.Extensions;

namespace TagsCloudVisualisation.Text.Formatting
{
    [VisibleName("Случайный шрифт")]
    public class RandomFontSizeResolver : IFontSizeResolver
    {
        private static readonly int[] sizes = Enumerable.Range(1, 10)
            .Select(i => i * 2)
            .ToArray();

        public IDictionary<string, float> GetFontSizesForAll(WordWithFrequency[] words) =>
            words.ToDictionary(w => w.Word, _ => (float) Randomized.ItemOf(sizes));
    }
}