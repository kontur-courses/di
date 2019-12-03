namespace TagsCloudContainer.WordPreprocessors
{
    class SimpleWordPreprocessor : IWordPreprocessor
    {
        public string WordPreprocessing(string word)
        {
            return word.ToLower();
        }
    }
}
