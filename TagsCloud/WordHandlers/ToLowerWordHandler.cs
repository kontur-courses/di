using TagsCloud.Interfaces;
using System.Text.RegularExpressions;

namespace TagsCloud.WordHandlers
{
    public class ToLowerWordHandler : IWordHandler
    {
        public string ProseccWord(string word)
        {
            var newWord = Regex.Replace(word, @"[^\w\.@-]", "", RegexOptions.None);
            return newWord.ToLowerInvariant();
        }
    }
}
