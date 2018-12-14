using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCloud.TextAnalyze.BlackList;

namespace WordCloud.TextAnalyze
{
    public class CommonWords : CommonBlacklist
    {
        private static readonly string[] TopCommonWords =
            new[]
            {
                "и",
                "в",
                "не",
                "он",
                "на",
                "я",
                "что",
                "тот",
                "быть",
                "с",
                "а",
                "весь",
                "это",
                "как",
                "она",
                "по",
                "но",
                "они",
                "к",
                "у",
                "ты",
                "из",
                "мы",
                "за",
                "вы",
                "так",
                "же",
                "от",
                "для",
                "этого"
            };

        public CommonWords()
        :base(TopCommonWords)
        {
            
        }

    }
}
