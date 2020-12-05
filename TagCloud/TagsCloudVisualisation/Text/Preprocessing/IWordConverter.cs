using System.Collections.Generic;

namespace TagsCloudVisualisation.Text.Preprocessing
{
    public interface IWordConverter
    {
        IEnumerable<string> Normalize(IEnumerable<string> words);
    }
}