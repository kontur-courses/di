using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public static class BoringWords
    {
        public static readonly List<string> Prepositions = new List<string>()
        {
            "без", "безо", "близ", "в", "во", "вместо", "вне","для", "до", "за", "из", "изо",
            "из-за", "из-под", "к", "ко", "кроме", "между", "меж", "на", "над", "надо", "о", "об", "обо",
            "от", "ото", "перед", "передо", "пред", "предо", "пo", "под", "подо", "при", "про", "ради", "с", "со",
            "сквозь", "среди", "у", "через", "чрез"
        };

        public static readonly List<string> Pronouns = new List<string>()
        {
            "я", "ты", "вы", "мы", "он", "оно", "она", "они", "себя", "кто", "что", "кто-то", "что-то", "кто-нибудь",
            "что-нибудь", "кто-либо", "что-либо", "кое-что", "кое-кто", "некто", "нечто", "никто", "ничто", "некого", "нечего"
        };
    }
}
