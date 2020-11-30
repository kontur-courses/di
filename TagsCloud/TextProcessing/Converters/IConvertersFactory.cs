using System.Collections.Generic;

namespace TagsCloud.TextProcessing.Converters
{
    public interface IConvertersFactory
    {
        public IEnumerable<string> ApplyConversion(IEnumerable<string> words, string[] converterNames);
        public IEnumerable<string> GetConverterNames();
    }
}
