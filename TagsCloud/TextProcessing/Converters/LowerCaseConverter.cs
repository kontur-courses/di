namespace TagsCloud.TextProcessing.Converters
{
    public class LowerCaseConverter : IWordConverter
    {
        public string Convert(string word) => word.ToLower();

        public string Name => "Перевести в нижний регистр";
    }
}
