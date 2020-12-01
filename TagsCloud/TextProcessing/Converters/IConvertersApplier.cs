using System.Collections.Generic;

namespace TagsCloud.TextProcessing.Converters
{
    public interface IConvertersApplier
    {
        IEnumerable<string> ApplyConversion(IEnumerable<string> words);
        IEnumerable<string> GetConverterNames();
    }
}
