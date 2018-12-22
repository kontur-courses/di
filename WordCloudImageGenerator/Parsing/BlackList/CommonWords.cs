namespace WordCloudImageGenerator.Parsing.BlackList
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
            : base(TopCommonWords)
        {
        }
    }
}