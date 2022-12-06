namespace TagCloudContainer.Filters
{

    public class FilterWords : IFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> textWords)
        {
            //var list = WordList.CreateFromWords(textWords);
            //return list.Suggest("").ToList();
            return textWords;
        }
    }
}
