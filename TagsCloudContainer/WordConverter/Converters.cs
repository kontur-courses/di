using System.Collections.Generic;

namespace TagsCloudContainer.WordConverter
{
    public class Converters
    {
        public static readonly Dictionary<string, IWordConverter> WordConvertersDictionary =
            new Dictionary<string, IWordConverter>
            {
                {"simple", new InitialFormWordConverter()}
            };
    }
}