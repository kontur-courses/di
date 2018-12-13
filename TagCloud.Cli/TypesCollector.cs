using System;
using System.Collections.Generic;
using TagCloud.Enums;
using TagCloud.Layouter.Spirals;

namespace TagCloud
{
    public static class TypesCollector
    {
        public static Dictionary<CloudLayouterType, Type> CollectLayouters()
        {
            return new Dictionary<CloudLayouterType, Type>
            {
                {CloudLayouterType.ArithmeticSpiral, typeof(ArithmeticSpiral)},
                {CloudLayouterType.SquareSpiral, typeof(SquareSpiral)}
            };
        }

        public static Dictionary<ColorScheme, Type> CollectColorSchemes()
        {
            return new Dictionary<ColorScheme, Type>
            {
                {ColorScheme.RandomColors, typeof(RandomColorScheme)},
                {ColorScheme.TransparentRed, typeof(RedColorScheme)}
            };
        }

        public static Dictionary<FontScheme, Type> CollectFontSchemes()
        {
            return new Dictionary<FontScheme, Type>
            {
                {FontScheme.Arial, typeof(ArialFontScheme)}
            };
        }

        public static Dictionary<SizeScheme, Type> CollectSizeSchemes()
        {
            return new Dictionary<SizeScheme, Type>
            {
                {SizeScheme.Linear, typeof(LinearSizeScheme)}
            };
        }
    }
}