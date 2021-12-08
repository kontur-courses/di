namespace CloudTagContainer
{
    public class CombinedPreprocessor: IWordsPreprocessor
    {
        private IWordsPreprocessor[] childPreprocessors;
        
        public CombinedPreprocessor(IWordsPreprocessor[] childPreprocessors)
        {
            this.childPreprocessors = childPreprocessors;
        }

        public string[] Preprocess(string[] rawWords)
        {
            var result = rawWords;
            foreach (var preprocessor in childPreprocessors)
            {
                result = preprocessor.Preprocess(result);
            }

            return result;
        }
    }
}