using System.Collections.Generic;

namespace TagsCloudVisualisation.Text.Preprocessing
{
    public interface IWordNormalizer
    {
        IEnumerable<string> Normalize(IEnumerable<string> words);
    }
}