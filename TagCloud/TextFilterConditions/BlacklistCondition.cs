using TagCloud.TextFiltration;

namespace TagCloud.TextFilterConditions
{
    public class BlacklistCondition : IFilterCondition
    {
        private readonly BlacklistMaker blacklistMaker;

        public BlacklistCondition(BlacklistMaker blacklistMaker)
        {
            this.blacklistMaker = blacklistMaker;
        }

        public bool CheckFilterCondition(string word)
        {
            return !blacklistMaker.BlackList.Contains(word);
        }
    }
}