using System.Collections.Generic;

namespace TagCloud.Validator
{
    public interface IWordsValidator
    {
        IEnumerable<string> Validate(IEnumerable<string> words, IEnumerable<string> boringWords);
    }
}