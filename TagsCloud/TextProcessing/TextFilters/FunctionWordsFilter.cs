namespace TagsCloud.TextProcessing.TextFilters
{
    public class FunctionWordsFilter : ITextFilter
    {
        public bool CanTake(string word) => word.Length > 3;

        public string Name => "Исключить служебные символы";
    }
}
