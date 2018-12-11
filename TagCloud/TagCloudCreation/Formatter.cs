using System.Linq;

namespace TagCloudCreation
{
    public class Formatter : IWordPreparer
    {
        public string PrepareWord(string word, TagCloudCreationOptions options)
        {
            var preparedWord = string.Join("", word.Trim()
                                                   .Where(char.IsLetter));
            return word == string.Empty ? null : preparedWord;
        }
    }
}
