using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordConverter
{
    public class Converters
    {
        private static readonly Dictionary<string, IWordConverter> WordConvertersDictionary =
            new Dictionary<string, IWordConverter>
            {
                {"simple", new InitialFormWordConverter()}
            };
        
        public static IWordConverter[] GetConvertersByName(IEnumerable<string> wordConverters)
        {
            return wordConverters.Select(converter => WordConvertersDictionary[converter]).ToArray();
        }
    }
}