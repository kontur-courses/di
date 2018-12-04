namespace TagCloud.Utility.Models.WordFilter
{
    public interface IWordFilter
    {
        string[] FilterWords(string[] words);

        void AddStopWord(string stopWord);
    }
}