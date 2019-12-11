namespace TagCloud.TextFilterConditions
{
    public class WordLengthCondition : IFilterCondition
    {
        public int WordMinLength { get; set; } = 3;

        public bool CheckFilterCondition(string word)
        {
            return word.Length >= WordMinLength;
        }
    }
}