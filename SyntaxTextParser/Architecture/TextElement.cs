namespace SyntaxTextParser.Architecture
{
    internal class TextElement
    {
        public readonly string Word;
        public readonly string Type;

        public TextElement(string word, string type)
        {
            Word = word;
            Type = type;
        }

        public CountedTextElement AddWordCount(int count) => new CountedTextElement(Word, Type, count);
    }
}
