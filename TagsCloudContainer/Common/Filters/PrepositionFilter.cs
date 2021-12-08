using System.Collections.Generic;
using TagsCloudContainer.Common.Contracts;

namespace TagsCloudContainer.Common.Filters
{
    public class PrepositionFilter : IWordFilter
    {
        private readonly HashSet<string> prepositions = new HashSet<string>
        {
            "без", "безо", "близ", "в", "во", "вместо", "вне", "для", "до", 
            "за", "из", "изо", "из-за", "из-под", "к", "ко", "кроме", "между", 
            "меж", "на", "над", "надо", "о", "об", "обо", "от", "ото", "перед", 
            "передо", "пред", "предо", "по", "под", "подо", "при", "про", "ради", 
            "с", "со", "сквозь", "среди", "у", "через", "чрез"
        };
        
        public bool IsValid(string word)
        {
            return !prepositions.Contains(word);
        }
    }
}