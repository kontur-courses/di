namespace TagCloud.Core.Util
{
    public class TagStat
    {
        public string Word { get; }
        public int RepeatsCount { get; }

        public TagStat(string word, int repeatsCount)
        {
            Word = word;
            RepeatsCount = repeatsCount;
        }
    }
}