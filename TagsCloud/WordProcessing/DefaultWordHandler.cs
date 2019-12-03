using TagsCloud.Interfaces;
using System.Text.RegularExpressions;

namespace TagsCloud.WordProcessing
{
    public class DefaultWordHandler : IWordHandler
    {
        public string ProseccWord(string word)
        {
            var newWord = Regex.Replace(word, @"[^\w\.@-]", "", RegexOptions.None);
            return newWord.ToLowerInvariant();
        }
    }
}
