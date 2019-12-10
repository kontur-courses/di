using System;
using System.Collections.Generic;
using TagsCloud.CloudLayouter;
using TagsCloud.FileReader;

namespace TagsCloud
{
    public static class TypesCollector
    {
        private static Dictionary<string, Type> Layouters = new Dictionary<string, Type>
        {
            {"CircularCloud", typeof(CircularCloudLayouter)},
            {"MiddleCloud", typeof(MiddleRectangleCloudLayouter) }
        };

        private static Dictionary<string, Type> Splitter = new Dictionary<string, Type>
        {
            {"Line", typeof(SpliterByLine) },
            {"WhiteSpace", typeof(SpliterByWhiteSpace) }
        };


        public static Type GetTypeGeneationLayoutersByName(string word)
        {
            Type type;
            Layouters.TryGetValue(word, out type);
            return type;
        }

        public static Type GetTypeSpliterByName(string word)
        {
            Type type;
            Splitter.TryGetValue(word, out type);
            return type;
        }
    }
}
