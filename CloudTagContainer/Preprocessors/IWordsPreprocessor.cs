namespace CloudTagContainer
{
    public interface IWordsPreprocessor
    {
        public string[] Preprocess(string[] rawWords);
    }
}