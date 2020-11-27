namespace TagsCloud.TextProcessing.TextFilters
{
    class FunctionWordsFilter : ITextFilter
    {
        public bool CanTake(string word) => word.Length > 3;
    }
}
