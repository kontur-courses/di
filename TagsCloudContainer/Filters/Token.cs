using System.Linq;
using Newtonsoft.Json.Linq;

namespace TagsCloudContainer.Filters
{
    public class Token
    {
        public string Value { get; }

        public WordType WordType { get; }

        public Token(string value, WordType wordType)
        {
            Value = value;
            WordType = wordType;
        }

        public static Token FromJson(JToken jToken)
        {
            WordType type;

            var stringType = jToken["analysis"].FirstOrDefault()?["gr"].ToString();
            
            stringType = stringType.Substring(0, stringType.IndexOfAny(new[] {'=', ' ', ','}));
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
                case "PR":
                    type = WordType.Preposition;
                    break;
                case "SPRO":
                    type = WordType.Preposition;
                    break;
                case "CONJ":
                    type = WordType.Conjunction;
                    break;
                default:
                    type = WordType.None;
                    break;
            }
            return new Token(jToken["text"].ToString(), type);
        }
    }
}
