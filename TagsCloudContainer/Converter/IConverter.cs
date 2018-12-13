using System.Collections.Generic;

namespace TagsCloudContainer.Converter
{
    public interface IConverter
    {
        IEnumerable<string> Convert(IEnumerable<string> words);
    }
}