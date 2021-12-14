namespace Visualization.Preprocessors
{
    public interface IWordsPreprocessor
    {
        public string[] Preprocess(string[] rawWords);
    }
}