namespace TagsCloudContainer.Dictionaries
{
    public interface IGrammarDictionary
    {
        bool ContainsWord(string word);
        bool TryGetCorrectForm(string word, out string correctForm);
    }
}