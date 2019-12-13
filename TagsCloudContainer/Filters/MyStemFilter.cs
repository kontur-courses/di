using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudContainer.Filters
{
    public class MyStemFilter : IFilter
    {
        private readonly GramPartsEnum[] allowedWorldType;
        private Mysteam mystem;

        public MyStemFilter(GramPartsEnum[] allowedWorldType, string pathToMyStem = null)
        {
            this.allowedWorldType = allowedWorldType;
            mystem = new Mysteam();

        }
        
        public IEnumerable<string> Filtering(IEnumerable<string> tokens)
        {
            var result =mystem.GetWords(string.Join(" ", tokens))
                .Where(el => allowedWorldType.Contains(el.Lexems[0].GramPart))
                .Select(t => t.SourceWord.Text)
                .Where(s => s.Length > 3);
            return result;
        }
    }
}