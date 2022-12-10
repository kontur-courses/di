namespace TagCloud.WordFilter
{
    public class BoringWordFilter : IWordFilter
    {
        public bool IsPermitted(string word)
        {
            return word.Length > 3;
        }
    }
}
