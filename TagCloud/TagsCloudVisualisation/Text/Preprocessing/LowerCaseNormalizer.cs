using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation.Text.Preprocessing
{
    public class LowerCaseNormalizer : IWordNormalizer
    {
        public IEnumerable<string> Normalize(IEnumerable<string> words) => words.Select(x => x.ToLower());
    }
}