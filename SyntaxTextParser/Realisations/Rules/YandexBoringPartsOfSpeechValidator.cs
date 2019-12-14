using System;
using System.Collections.Generic;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class YandexBoringPartsOfSpeechValidator : IElementValidator
    {
        public static HashSet<string> PatternPartsOfSpeech { get; } = new HashSet<string>
        {
            "A", "ADV", "ADVPRO", "ANUM", "APRO", "COM", "CONJ", "INTJ", "NUM", "PART", "PR", "S", "SPRO", "V"
        };

        private readonly HashSet<string> boringPartsOfSpeech;

        public YandexBoringPartsOfSpeechValidator(params string[] partsOfSpeech)
        {
            foreach (var str in partsOfSpeech)
            {
                if(!PatternPartsOfSpeech.Contains(str))
                    throw new ArgumentException($"{str} don't contains in Yandex parts of speech");
            }

            boringPartsOfSpeech = new HashSet<string>(partsOfSpeech);
        }

        public bool IsValidElement(TypedTextElement element)
        {
            return !boringPartsOfSpeech.Contains(element.Type);
        }
    }
}