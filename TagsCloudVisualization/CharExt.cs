using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public static class CharExt
    {
        private const double DefaultSize = 22d / 30;

        private static readonly Dictionary<char, float> CharsSize = new()
        {
            
            ['ш'] = 1,
            ['щ'] = 1,
            ['ы'] = 1,
            ['ж'] = 1,
            ['м'] = 1,
            ['ю'] = 1,
            ['ф'] = 1,
        };

        public static double GetSize(this char c) => CharsSize.TryGetValue(c, out var r) ? r : DefaultSize;
    }
}