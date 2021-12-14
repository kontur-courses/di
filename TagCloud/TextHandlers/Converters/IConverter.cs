using System.Collections.Generic;

namespace TagCloud.TextHandlers.Converters
{
    public interface IConverter
    {
       string Convert(string word);
    }
}