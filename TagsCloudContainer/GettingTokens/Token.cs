using Newtonsoft.Json.Linq;

namespace TagsCloudContainer.GettingTokens
{
    public class Token
    {
        public readonly string Word;
        public readonly WordType WordType;

        public Token(string word, WordType type)
        {
            Word = word;
            WordType = type;
        }

        public static Token FromJson(JToken token)
        {
            var type = WordType.None;

            var stringType = token["gr"].ToString();
            stringType = stringType
                .Substring(0, stringType.IndexOfAny(new[] {'=', ' ', ','}));
            switch (stringType)
            {
                case "A":
                    type = WordType.Adjective;
                    break;
                case "S":
                    type = WordType.Noun;
                    break;
                case "V":
                    type = WordType.Verb;
                    break;
            }

            return new Token(token["lex"].ToString(), type);
        }

        public override string ToString()
        {
            return $"word: {Word}; type: {WordType}";
        }
    }
}