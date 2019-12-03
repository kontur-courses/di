namespace TagCloud.TextFilter
{
    public class TextFilterSettings
    {
        public int WordMinLength { get; set; } = 3;

        public readonly char[] Separators = {' ', '"', '(', ')', '.', '!', '?', '\'', ','};
    }
}