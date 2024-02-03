using MyStemWrapper;

namespace TagsCloud.WordValidators;

public class SimpleWordValidator : IWordValidator
{
    public bool IsWordValid(string word)
    {
        return word.Length > 3;
    }
}