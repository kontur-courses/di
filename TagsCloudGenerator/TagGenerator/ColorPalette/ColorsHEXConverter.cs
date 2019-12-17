using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudGenerator
{
    public static class ColorsHexConverter
    {
        public static Color[] CreateFromHexEnumerable(string hecEnumerable)
        {
            if(string.IsNullOrWhiteSpace(hecEnumerable))
                throw new ArgumentException("Can't create palette from incorrect string");

            return hecEnumerable.Split(' ')
                .Select(CreateFromHex).ToArray();
        }

        public static Color CreateFromHex(string hec) => ColorTranslator.FromHtml(hec);

    }
}