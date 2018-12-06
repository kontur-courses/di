namespace TagCloud.Utility.Models.WordFilter
{
    public interface IWordFilter
    {
        string[] FilterWords(string[] words);

        void Add(string stopWord);
    }
}